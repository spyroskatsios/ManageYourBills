using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillTypesQueries;
public record GetBillTypesQuery : IRequest<IEnumerable<string>>;