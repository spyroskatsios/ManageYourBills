using ManageYourBills.Application.Commands.ProviderCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Validators.Provider;
public class DeleteProviderValidator : AbstractValidator<DeleteProviderCommand>
{
    public DeleteProviderValidator()
    {
        RuleFor(x => x.Id)
          .NotEmpty();
    }
}
