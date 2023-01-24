using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillQueries.Handlers;
public class GetBillPaymentsHandler : IRequestHandler<GetBillPaymentsQuery, IEnumerable<BillPaymentResponse>>
{
    private readonly IAppDbContext _context;

    public GetBillPaymentsHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BillPaymentResponse>> Handle(GetBillPaymentsQuery request, CancellationToken cancellationToken)
      => await _context.Bills
             .Where(x => x.Issued >= request.From && x.Issued <= request.To && x.Type == (BillType)Enum.Parse(typeof(BillType), request.Type))
             .Select(x => new BillPaymentResponse(x.Issued, x.Cost)).ToListAsync(cancellationToken);

}
