using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillQueries.Handlers;
public class GetBillByIdHandler : IRequestHandler<GetBillByIdQuery, BillResponse?>
{
    private readonly IAppDbContext _context;

    public GetBillByIdHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<BillResponse?> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
     => (await _context.Bills
            .AsNoTracking()
            .Include(x => x.Provider)
            .FirstOrDefaultAsync(x => x.Id == new BillId(request.Id), cancellationToken))
            .ToBillResponse();

}
