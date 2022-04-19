using ManageYourBills.Application.Exceptions;
using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.BillCommands.Handlers;
public class UpdateBillHandler : IRequestHandler<UpdateBillCommand, bool>
{
    private readonly IAppDbContext _context;

    public UpdateBillHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
    {
        var bill = await _context.Bills
            .Include(x => x.Provider)
            .FirstOrDefaultAsync(x => x.Id == new BillId(request.Id));

        if (bill is null)
        {
            return false;
        }

        var provider = await _context.Providers.FindAsync(new ProviderId(request.ProviderId));

        if (provider is null)
        {
            throw new ProviderNotFoundException();
        }

        bill.Provider = provider;
        bill.Cost = request.Cost;
        bill.Type = (BillType)Enum.Parse(typeof(BillType), request.Type);
        bill.Comments = request.Comments;
        bill.Cost = request.Cost;
        bill.IsPaid = request.IsPaid;

        bill.ChangeDates(request.Issued, request.From, request.To, request.Expiration, request.PaidAt);

        _context.Bills.Update(bill);
        await _context.SaveAsync();
        return true;
    }
}
