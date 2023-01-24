using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Infrastructure.Persistence.Configuration;
public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(id => id.Value, id => new ProviderId(id));

        builder
            .Property(x => x.Name)
            .HasConversion(name => name.Value, name => new ProviderName(name))
            .HasMaxLength(100);
    }
}
