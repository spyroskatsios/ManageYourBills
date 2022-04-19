using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.BlazorServer.Models;
public class ProviderModel
{
    public Guid Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public ProviderModel(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public ProviderModel()
    {

    }
}
