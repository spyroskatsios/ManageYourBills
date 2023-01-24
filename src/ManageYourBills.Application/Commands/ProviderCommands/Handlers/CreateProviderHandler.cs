using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.ProviderCommands.Handlers;
public class CreateProviderHandler : IRequestHandler<CreateProviderCommand, Guid>
{
    private readonly IAppDbContext _context;

    public CreateProviderHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateProviderCommand request, CancellationToken cancellationToken)
    {
        var provider = new Provider(Guid.NewGuid(), request.Name);
        await _context.Providers.AddAsync(provider);
        await _context.SaveAsync();
        return provider.Id;
    }
}
