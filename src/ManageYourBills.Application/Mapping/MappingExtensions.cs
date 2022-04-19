using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Mapping;
public static class MappingExtensions
{
    public static ProviderResponse? ToProviderResponse(this Provider provider)
        => provider is null ? null : new ProviderResponse(provider.Id, provider.Name.Value, provider.Archived);

    public static BillResponse? ToBillResponse(this Bill? bill)
        => bill is null ? null : new BillResponse(bill.Id, bill.Type.ToString(), bill.Provider.ToProviderResponse(), bill.Issued,
            bill.From, bill.To, bill.Expiration, bill.Comments, bill.Cost, bill.IsPaid, bill.PaidAt);


    public static List<ProviderResponse> ToProvidersResponse(this IList<Provider> providers)
    {
        var output = new List<ProviderResponse>();

        foreach (var provider in providers)
        {
            output.Add(provider.ToProviderResponse());
        }

        return output;
    }

    public static List<BillResponse> ToBillsResponse(this IEnumerable<Bill> bills)
    {
        var output = new List<BillResponse>();

        foreach (var bill in bills)
        {
            output.Add(bill.ToBillResponse());
        }

        return output;
    }
}
