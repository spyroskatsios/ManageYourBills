using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.BillCommands;
public record CreateBillCommand(string Type, Guid ProviderId, DateOnly Issue, DateOnly From, DateOnly To, DateOnly Expiration,
    string Comments, decimal Cost, bool IsPaid, DateOnly? PaidAt) : IRequest<Guid>;
