using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Infrastructure.Persistence.Configuration;
public class BillConfiguration : IEntityTypeConfiguration<Bill>
{
    public void Configure(EntityTypeBuilder<Bill> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasConversion(id => id.Value, id => new BillId(id));

        builder
            .HasOne(x => x.Provider)
            .WithMany();

        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(x => x.ToDateTime(TimeOnly.MinValue), x => DateOnly.FromDateTime(x));

        builder
            .Property(x => x.Issued)
            .HasConversion(dateOnlyConverter);

        builder
            .Property(x => x.From)
            .HasConversion(dateOnlyConverter);

        builder
            .Property(x => x.To)
            .HasConversion(dateOnlyConverter);

        builder
            .Property(x => x.Expiration)
            .HasConversion(dateOnlyConverter);

        var nullableDateConverter = new ValueConverter<DateOnly?, DateTime?>(x => x == null ? null : x.Value.ToDateTime(TimeOnly.MinValue),
            x => x == null ? null : DateOnly.FromDateTime(x.Value));

        builder
            .Property(x => x.PaidAt)
            .HasConversion(nullableDateConverter);

        builder
            .Property(x => x.Cost)
            .HasColumnType("real")
            .HasConversion<double>();

        builder
            .Property(x => x.Comments)
            .HasMaxLength(500);
    }
}
