using ManageYourBills.Application.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Queries.BillQueries;
public record GetBillsQuery : IRequest<PagedList<BillResponse>>
{
    private const int maxPageSize = 50;

    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;

        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }

    public string? BillType { get; set; }
    public Guid? ProviderId { get; set; }
    public DateOnly? ExpirationFrom { get; set; }
    public DateOnly? ExpirationTo { get; set; }
    public DateOnly? PaidAtFrom { get; set; }
    public DateOnly? PaidAtTo { get; set; }
    public decimal? CostFrom { get; set; }
    public decimal? CostTo { get; set; }
    public string SortOrder { get; set; } = "Descending";
    public string OrderParameter { get; set; } = "Cost";

}
