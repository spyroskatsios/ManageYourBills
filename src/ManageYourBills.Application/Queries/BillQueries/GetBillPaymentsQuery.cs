using ManageYourBills.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillQueries;
public record GetBillPaymentsQuery(string Type, DateOnly From, DateOnly To) : IRequest<IEnumerable<BillPaymentResponse>>;
