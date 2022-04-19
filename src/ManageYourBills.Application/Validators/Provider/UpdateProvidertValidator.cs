using ManageYourBills.Application.Commands.ProviderCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Validators.Provider;
public class UpdateProvidertValidator : AbstractValidator<UpdateProviderCommand>
{
    public UpdateProvidertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}

