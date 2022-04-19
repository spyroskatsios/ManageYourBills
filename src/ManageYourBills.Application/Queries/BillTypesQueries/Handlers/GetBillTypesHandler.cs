using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillTypesQueries.Handlers;
public class GetBillTypesHandler : IRequestHandler<GetBillTypesQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetBillTypesQuery request, CancellationToken cancellationToken)
    {
        var values = Enum.GetValues(typeof(BillType));

        var types = new List<string>();

        foreach (var value in values)
        {
            types.Add(value.ToString());
        }

        return await Task.FromResult(types);
    }
}
