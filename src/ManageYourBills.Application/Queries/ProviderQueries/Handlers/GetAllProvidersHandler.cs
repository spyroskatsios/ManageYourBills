using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.ProviderQueries.Handlers;
public class GetAllProvidersHandler : IRequestHandler<GetAllProvidersQuery, IEnumerable<ProviderResponse>>
{
    private readonly IAppDbContext _context;

    public GetAllProvidersHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProviderResponse>> Handle(GetAllProvidersQuery request, CancellationToken cancellationToken)
     => (await _context.Providers.AsNoTracking().ToListAsync(cancellationToken)).ToProvidersResponse();
}
