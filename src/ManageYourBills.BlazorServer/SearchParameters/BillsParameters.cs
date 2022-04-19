using ManageYourBills.BlazorServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.BlazorServer.SearchParameters;
public class BillsParameters
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? BillType { get; set; }
    public ProviderModel? Provider { get; set; }
    public DateTime? ExpirationFrom { get; set; }
    public DateTime? ExpirationTo { get; set; }
    public DateTime? PaidAtFrom { get; set; }
    public DateTime? PaidAtTo { get; set; }
    public decimal? CostFrom { get; set; }
    public decimal? CostTo { get; set; }
    public string SortOrder { get; set; } = "Descending";
    public string OrderParameter { get; set; } = "PaidAt";
}
