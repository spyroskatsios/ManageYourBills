using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Responses;
public record BillResponse(Guid Id, string Type, ProviderResponse Provider, DateOnly Issued, DateOnly From, DateOnly To, DateOnly Expiration,
    string Comments, decimal Cost, bool IsPaid, DateOnly? PaidAt);
