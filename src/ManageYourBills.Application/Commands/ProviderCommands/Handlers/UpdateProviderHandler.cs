using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.ProviderCommands.Handlers;
public class UpdateProviderHandler : IRequestHandler<UpdateProviderCommand, bool>
{
    private readonly IAppDbContext _context;

    public UpdateProviderHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = await _context.Providers.FindAsync(new ProviderId(request.Id));

        if (provider is null)
        {
            return false;
        }

        provider.Name = request.Name;
        await _context.SaveAsync();
        return true;

    }
}
