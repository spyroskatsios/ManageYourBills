using ManageYourBills.Application.Queries.BillQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Validators.Bill;
public class GetBillPaymentsValidator : AbstractValidator<GetBillPaymentsQuery>
{
    public GetBillPaymentsValidator()
    {
        RuleFor(x => x.From)
            .LessThan(x => x.To);

        RuleFor(x => x.Type)
            .IsEnumName(typeof(BillType), caseSensitive: false);
    }
}
