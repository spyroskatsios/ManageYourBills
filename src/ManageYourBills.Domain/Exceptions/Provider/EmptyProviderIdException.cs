using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Exceptions.Provider;
public class EmptyProviderIdException : ProviderException
{
    public EmptyProviderIdException() : base("Provider Id can not be empty.")
    {
    }
}
