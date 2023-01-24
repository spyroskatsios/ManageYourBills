using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.StatsQueries;
public record GetCostForDateRangeQuery(DateOnly From, DateOnly To, string Type, Guid? ProviderId) : IRequest<IDictionary<DateOnly, decimal>>;
