using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Exceptions.Bill;
public abstract class BillException : Exception
{
    protected BillException(string message) : base(message) { }
}
