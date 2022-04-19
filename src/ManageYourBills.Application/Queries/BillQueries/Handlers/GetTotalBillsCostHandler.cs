using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillQueries.Handlers;
public class GetTotalBillsCostHandler : IRequestHandler<GetTotalBillsCostQuery, IEnumerable<TotalTypeCostResponse>>
{
    private readonly IAppDbContext _context;

    public GetTotalBillsCostHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TotalTypeCostResponse>> Handle(GetTotalBillsCostQuery request, CancellationToken cancellationToken)
    => await _context.Bills
        .Where(x => x.Issued >= request.From && x.Issued <= request.To)
        .GroupBy(x => x.Type)
        .Select(x => new TotalTypeCostResponse(x.First().Type.ToString(), x.Sum(c => c.Cost)))
        .ToListAsync(cancellationToken);
}
