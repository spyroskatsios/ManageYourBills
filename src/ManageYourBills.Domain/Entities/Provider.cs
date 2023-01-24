using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Entities;
public class Provider
{
    public ProviderId Id { get; private set; }

    public ProviderName Name { get; set; }

    public bool Archived { get; set; }

    public Provider(Guid id, string name)
    {
        Id = id;
        Name = name;
        Archived = false;
    }

    private Provider()
    {
    }
}

