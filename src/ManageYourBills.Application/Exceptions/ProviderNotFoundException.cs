using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Exceptions;
public class ProviderNotFoundException : Exception
{
    public ProviderNotFoundException() : base("Could not find Provider.")
    {

    }
}
