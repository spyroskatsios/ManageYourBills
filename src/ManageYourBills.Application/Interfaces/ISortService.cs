using ManageYourBills.Application.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Interfaces;
public interface ISortService
{
    IQueryable<Bill> Sort(IQueryable<Bill> bills, SortOrder sortOrder, BillOrderParameters orderParameter);
}
