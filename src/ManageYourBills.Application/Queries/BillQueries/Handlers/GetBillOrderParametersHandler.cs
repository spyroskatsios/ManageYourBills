using ManageYourBills.Application.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillQueries.Handlers;
public class GetBillOrderParametersHandler : IRequestHandler<GetBillOrderParametersQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetBillOrderParametersQuery request, CancellationToken cancellationToken)
    {
        var values = Enum.GetValues(typeof(BillOrderParameters));

        var types = new List<string>();

        foreach (var value in values)
        {
            types.Add(value.ToString());
        }

        return await Task.FromResult(types);
    }
}
