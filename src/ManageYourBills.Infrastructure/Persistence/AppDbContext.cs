using ManageYourBills.Application.Interfaces;
using ManageYourBills.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Infrastructure.Persistence;
public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Provider> Providers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("ManageYourBills");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public async Task SaveAsync()
    => await SaveChangesAsync();
}
