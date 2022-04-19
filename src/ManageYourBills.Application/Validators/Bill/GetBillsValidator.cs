using ManageYourBills.Application.Queries.BillQueries;
using ManageYourBills.Application.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Validators.Bill;
public class GetBillsValidator : AbstractValidator<GetBillsQuery>
{
    public GetBillsValidator()
    {
        RuleFor(x => x.SortOrder)
            .IsEnumName(typeof(SortOrder), false);

        RuleFor(x => x.OrderParameter)
           .IsEnumName(typeof(BillOrderParameters), false);
    }
}
