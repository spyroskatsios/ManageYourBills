using ManageYourBills.Application.RequestFeatures;
using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillQueries.Handlers;
public class GetBillsHandler : IRequestHandler<GetBillsQuery, PagedList<BillResponse>>
{
    private readonly IAppDbContext _context;
    private readonly ISortService _sortService;

    public GetBillsHandler(IAppDbContext context, ISortService sortService)
    {
        _context = context;
        _sortService = sortService;
    }

    public async Task<PagedList<BillResponse>> Handle(GetBillsQuery request, CancellationToken cancellationToken)
    {
        var queryable = GetBillsQuerable(request);

        var bills = await queryable
              .Include(x => x.Provider)
              .Skip((request.PageNumber - 1) * request.PageSize)
              .Take(request.PageSize)
              .ToListAsync(cancellationToken);

        var count = await queryable.CountAsync(cancellationToken);

        var billsResponse = bills.ToBillsResponse();

        return PagedList<BillResponse>.ToPagedList(billsResponse, request.PageNumber, request.PageSize, count);
    }

    private IQueryable<Bill> GetBillsQuerable(GetBillsQuery request)
    {
        var bills = _context.Bills.AsNoTracking();

        if (request.BillType is not null) bills = bills.Where(x => x.Type == (BillType)Enum.Parse(typeof(BillType), request.BillType));
        if (request.ProviderId is not null) bills = bills.Where(x => x.Provider.Id == new ProviderId(request.ProviderId.Value));
        if (request.ExpirationFrom is not null) bills = bills.Where(x => x.Expiration >= request.ExpirationFrom.Value);
        if (request.ExpirationTo is not null) bills = bills.Where(x => x.Expiration <= request.ExpirationTo.Value);
        if (request.PaidAtFrom is not null) bills = bills.Where(x => x.PaidAt >= request.PaidAtFrom.Value);
        if (request.PaidAtTo is not null) bills = bills.Where(x => x.Expiration <= request.PaidAtTo.Value);
        if (request.CostFrom is not null) bills = bills.Where(x => x.Cost >= request.CostFrom.Value);
        if (request.CostTo is not null) bills = bills.Where(x => x.Cost <= request.CostTo.Value);

        return _sortService.Sort(bills, (SortOrder)Enum.Parse(typeof(SortOrder), request.SortOrder), (BillOrderParameters)Enum.Parse(typeof(BillOrderParameters), request.OrderParameter));

    }
}
