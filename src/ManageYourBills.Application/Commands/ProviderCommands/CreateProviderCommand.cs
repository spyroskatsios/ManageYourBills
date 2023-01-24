using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Commands.ProviderCommands;
public record CreateProviderCommand(string Name) : IRequest<Guid>;
