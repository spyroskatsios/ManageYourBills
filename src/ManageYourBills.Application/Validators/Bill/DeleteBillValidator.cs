using ManageYourBills.Application.Commands.BillCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Validators.Bill;
public class DeleteBillValidator : AbstractValidator<DeleteBillCommand>
{
    public DeleteBillValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
