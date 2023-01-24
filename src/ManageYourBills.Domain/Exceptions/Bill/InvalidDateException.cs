using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Exceptions.Bill;
public class InvalidDateException : BillException
{
    public InvalidDateException(string message) : base(message)
    {
    }
}
