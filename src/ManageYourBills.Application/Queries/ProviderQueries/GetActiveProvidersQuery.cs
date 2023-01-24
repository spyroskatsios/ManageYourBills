using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.ProviderQueries;
public record GetActiveProvidersQuery : IRequest<IEnumerable<ProviderResponse>>;
