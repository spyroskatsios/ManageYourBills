using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.ValueObjects;
public record ProviderId
{
    public Guid Value { get; set; }

    public ProviderId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyProviderIdException();
        }

        Value = value;
    }

    public static implicit operator Guid(ProviderId providerId)
        => providerId.Value;

    public static implicit operator ProviderId(Guid providerId)
        => new(providerId);
}
