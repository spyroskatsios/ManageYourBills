using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.ValueObjects;
public record ProviderName
{
    public string? Value { get; set; }

    public ProviderName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new EmptyorNullProviderNameException();
        }

        if (value.Length > 100)
        {
            throw new TooLongProviderNameException(100);
        }

        Value = value;
    }

    public static implicit operator string(ProviderName providerName)
      => providerName.Value;

    public static implicit operator ProviderName(string providerName)
        => new(providerName);
}
