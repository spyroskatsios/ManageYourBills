using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillQueries;
public record GetTotalBillsCostQuery(DateOnly From, DateOnly To) : IRequest<IEnumerable<TotalTypeCostResponse>>;
