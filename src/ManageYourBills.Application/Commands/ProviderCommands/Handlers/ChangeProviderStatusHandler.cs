using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.ProviderCommands.Handlers;
public class ChangeProviderStatusHandler : IRequestHandler<ChangeProviderStatusCommand, bool>
{
    private readonly IAppDbContext _context;

    public ChangeProviderStatusHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(ChangeProviderStatusCommand request, CancellationToken cancellationToken)
    {
        var provider = await _context.Providers.FindAsync(new ProviderId(request.Id));

        if (provider is null)
        {
            return false;
        }

        provider.Archived = !provider.Archived;
        await _context.SaveAsync();
        return true;
    }
}
