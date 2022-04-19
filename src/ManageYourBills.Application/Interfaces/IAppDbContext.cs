using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Application.Interfaces;
public interface IAppDbContext
{
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Provider> Providers { get; set; }

    Task SaveAsync();
}
