using ManageYourBills.Application.Exceptions;
using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.BillCommands.Handlers;
public class CreateBillHandler : IRequestHandler<CreateBillCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateBillHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        var provider = await _context.Providers.FindAsync(new ProviderId(request.ProviderId));

        if (provider is null)
        {
            throw new ProviderNotFoundException();
        }

        var bill = new Bill(Guid.NewGuid(), (BillType)Enum.Parse(typeof(BillType), request.Type), provider, request.Issue, request.From, request.To, request.Expiration,
            request.Comments, request.Cost, request.IsPaid, request.PaidAt);

        await _context.Bills.AddAsync(bill);
        await _context.SaveAsync();
        return bill.Id;
    }
}
