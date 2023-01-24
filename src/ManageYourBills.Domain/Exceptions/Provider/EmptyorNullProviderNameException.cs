using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Exceptions.Provider;
public class EmptyorNullProviderNameException : ProviderException
{
    public EmptyorNullProviderNameException() : base("Provider Name can not be null or empty")
    {
    }
}
