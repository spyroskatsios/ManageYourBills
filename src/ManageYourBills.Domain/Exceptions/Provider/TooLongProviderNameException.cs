using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Exceptions.Provider;
public class TooLongProviderNameException : ProviderException
{
    public TooLongProviderNameException(int maxLenght) : base($"Provider can not be more than {maxLenght} characters.")
    {
    }
}
