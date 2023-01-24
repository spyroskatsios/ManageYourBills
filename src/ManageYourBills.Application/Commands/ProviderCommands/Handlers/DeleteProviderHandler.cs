using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.ProviderCommands.Handlers;
public class DeleteProviderHandler : IRequestHandler<DeleteProviderCommand, bool>
{
    private readonly IAppDbContext _context;

    public DeleteProviderHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await _context.Providers.FindAsync(new ProviderId(request.Id));

        if (provider is null)
        {
            return false;
        }

        _context.Providers.Remove(provider);
        await _context.SaveAsync();
        return true;
    }
}
