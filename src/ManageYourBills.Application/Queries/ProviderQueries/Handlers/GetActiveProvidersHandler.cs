using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.ProviderQueries.Handlers;
public class GetActiveProvidersHandler : IRequestHandler<GetActiveProvidersQuery, IEnumerable<ProviderResponse>>
{
    private readonly IAppDbContext _context;

    public GetActiveProvidersHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProviderResponse>> Handle(GetActiveProvidersQuery request, CancellationToken cancellationToken)
     => (await _context.Providers.AsNoTracking().Where(x => x.Archived == false).ToListAsync(cancellationToken)).ToProvidersResponse();
}
