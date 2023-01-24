using FluentValidation.Results;
using ManageYourBills.Application.Commands.BillCommands;
using ManageYourBills.Application.Commands.ProviderCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Validators.Bill;
public class CreateBillValidator : AbstractValidator<CreateBillCommand>
{
    public CreateBillValidator()
    {
        RuleFor(x => x.ProviderId)
            .NotEmpty();

        RuleFor(x => x.From)
            .LessThan(x => x.To);

        RuleFor(x => x.From)
            .LessThan(x => x.Issue);

        //RuleFor(x => x.PaidAt)
        //    .LessThan(x => x.PaidAt);

        RuleFor(x => x.Comments)
            .MaximumLength(500);

        RuleFor(x => x.Type)
            .IsEnumName(typeof(BillType), caseSensitive: false);

        RuleFor(x => x.PaidAt)
            .NotNull()
            .When(x => x.IsPaid);
    }
}
