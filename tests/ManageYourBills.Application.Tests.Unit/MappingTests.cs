using FluentAssertions;
using ManageYourBills.Application.Mapping;
using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ManageYourBills.Application.Tests.Unit;
public class MappingTests
{
    [Fact]
    public void ToProviderResponse_ShouldMap()
    {
        // Arrange
        var provider = new Provider(Guid.NewGuid(), "qw");
        provider.Archived = true;

        // Act
        var result = provider.ToProviderResponse();

        // Assert
        result.Id.Should().Be(provider.Id.Value);
        result.Name.Should().Be(provider.Name.Value);
        result.Archived.Should().Be(provider.Archived);
    }

    [Fact]
    public void ToBillResponse_ShouldMap()
    {
        // Arrange

        var bill = new Bill(Guid.NewGuid(), BillType.Tv, new Provider(Guid.NewGuid(), "qw"),
        DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5)), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-40)),
        DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10)), DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20)),
        null, 10, true, DateOnly.FromDateTime(DateTime.UtcNow));

        // Act
        var result = bill.ToBillResponse();

        // Assert
        result.Id.Should().Be(bill.Id.Value);
        result.Type.Should().Be(bill.Type.ToString());
        result.Issued.Should().Be(bill.Issued);
        result.From.Should().Be(bill.From);
        result.To.Should().Be(bill.To);
        result.Expiration.Should().Be(bill.Expiration);
        result.Comments.Should().Be(bill.Comments);
        result.Cost.Should().Be(bill.Cost);
        result.IsPaid.Should().Be(bill.IsPaid);
        result.PaidAt.Should().Be(bill.PaidAt);
    }
}
