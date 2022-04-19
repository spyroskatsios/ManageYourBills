using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Exceptions.Provider;
public abstract class ProviderException : Exception
{
    protected ProviderException(string message) : base(message) { }
}
