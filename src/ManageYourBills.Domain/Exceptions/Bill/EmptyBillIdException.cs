using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Exceptions.Bill;
public class EmptyBillIdException : BillException
{
    public EmptyBillIdException() : base("Bill Id can not be empty.")
    {
    }
}
