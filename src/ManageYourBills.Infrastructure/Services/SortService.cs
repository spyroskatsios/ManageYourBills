using ManageYourBills.Application.Interfaces;
using ManageYourBills.Application.RequestFeatures;
using ManageYourBills.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Infrastructure.Services;
public class SortService : ISortService
{
    public IQueryable<Bill> Sort(IQueryable<Bill> bills, SortOrder sortOrder, BillOrderParameters orderParameter)
        => sortOrder == SortOrder.Descending ? BillsDescending(bills, orderParameter) : BillsAscending(bills, orderParameter);

    private IQueryable<Bill> BillsAscending(IQueryable<Bill> bills, BillOrderParameters orderParameter)
    {
        return orderParameter switch
        {
            BillOrderParameters.Expiration => bills.OrderBy(x => x.Expiration),
            BillOrderParameters.Cost => bills.OrderBy(x => x.Cost),
            _ => bills.OrderBy(x => x.PaidAt)
        };
    }

    private IQueryable<Bill> BillsDescending(IQueryable<Bill> bills, BillOrderParameters orderParameter)
    {
        return orderParameter switch
        {
            BillOrderParameters.Expiration => bills.OrderByDescending(x => x.Expiration),
            BillOrderParameters.Cost => bills.OrderByDescending(x => x.Cost),
            _ => bills.OrderByDescending(x => x.PaidAt)
        };
    }
}
