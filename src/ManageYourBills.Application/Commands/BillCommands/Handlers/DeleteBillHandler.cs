using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.BillCommands.Handlers;
public class DeleteBillHandler : IRequestHandler<DeleteBillCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteBillHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
    {
        var bill = await _context.Bills.FindAsync(new BillId(request.Id));

        if (bill is null)
        {
            return false;
        }

        _context.Bills.Remove(bill);
        await _context.SaveAsync();
        return true;
    }
}
