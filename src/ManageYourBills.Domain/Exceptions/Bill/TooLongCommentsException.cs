using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Exceptions.Bill;
public class TooLongCommentsException : BillException
{
    public TooLongCommentsException() : base("Comments can not be more than 500 characters.")
    {

    }
}
