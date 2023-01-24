using ManageYourBills.Application.Queries.BillQueries;
using ManageYourBills.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration.Queries.BillQueries;

[Collection("TestBase")]
public class GetBillsTests : IAsyncLifetime
{
    private readonly TestBase _base;
    private List<Bill> bills;
    private List<Provider> providers;

    private DateOnly issued;
    private DateOnly from;
    private DateOnly to;
    private DateOnly? paidAt;
    private DateOnly expires;

    public GetBillsTests(TestBase @base)
    {
        _base = @base;
        _base.ResetState();

        issued = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5));
        from = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-40));
        to = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10));
        expires = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20));
        paidAt = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    public async Task InitializeAsync()
    {

        providers = await _base.CreateProvidersAsync();

        bills = new List<Bill>
        {
            new Bill(Guid.NewGuid(), BillType.Tv, providers[0], issued.AddYears(1), from.AddYears(1), to.AddYears(1),
                expires.AddYears(1),  string.Empty, 25, true, paidAt.Value.AddYears(1)),
            new Bill(Guid.NewGuid(), BillType.Tv, providers[1], issued, from, to, expires,  string.Empty, 25, true, paidAt),
            new Bill(Guid.NewGuid(), BillType.Electricity, providers[0], issued, from, to, expires,  string.Empty, 25, true, paidAt),
            new Bill(Guid.NewGuid(), BillType.Water, providers[1], issued, from, to, expires,  string.Empty, 30, true, paidAt),
            new Bill(Guid.NewGuid(), BillType.Mobile, providers[2], issued, from, to, expires,  string.Empty, 25, true, paidAt),
            new Bill(Guid.NewGuid(), BillType.Mobile, providers[2], issued, from, to, expires,  string.Empty, 50, true, paidAt),
            new Bill(Guid.NewGuid(), BillType.Electricity, providers[0], issued, from, to, expires,  string.Empty, 25, true, paidAt),
            new Bill(Guid.NewGuid(), BillType.Water, providers[0], issued, from, to, expires,  string.Empty, 25, true, paidAt),
            new Bill(Guid.NewGuid(), BillType.Tv, providers[0], issued, from, to, expires,  string.Empty, 40, false, null),
            new Bill(Guid.NewGuid(), BillType.Tv, providers[0], issued.AddYears(1), from.AddYears(1), to.AddYears(1),
                expires.AddYears(1),  string.Empty, 100, false, null),
        };

        foreach (var bill in bills)
        {
            await _base.CreateBillWithExistingProviderAsync(bill);
        }

    }

    [Fact]
    public async Task GetBills_ShouldGetBills()
    {
        // Arrange
        var query = new GetBillsQuery();

        // Act
        var bills = await _base.SendAsync(query);

        // Assert
        bills.Count.Should().Be(10);
        bills.MetaData.HasNextPage.Should().BeFalse();
    }

    [Fact]
    public async Task GetBills_WithPageSizeFive_ShouldGetFiveBills()
    {
        // Arrange
        var query = new GetBillsQuery { PageSize = 5 };

        // Act
        var bills = await _base.SendAsync(query);

        // Assert
        bills.Count.Should().Be(5);
        bills.MetaData.HasNextPage.Should().BeTrue();
        bills.MetaData.TotalCount.Should().Be(10);
    }

    [Fact]
    public async Task GetBills_WithProvider_ShouldGetBillsWithProvider()
    {
        // Arrange
        var query = new GetBillsQuery { ProviderId = providers[0].Id.Value };

        // Act
        var bills = await _base.SendAsync(query);

        // Assert
        bills.Count.Should().Be(6);
        bills.MetaData.HasNextPage.Should().BeFalse();
    }

    [Fact]
    public async Task GetBills_WithCostLimit_ShouldGetBillsWithCostLimit()
    {
        // Arrange
        var query = new GetBillsQuery { ProviderId = providers[0].Id.Value, CostFrom = 99 };

        // Act
        var bills = await _base.SendAsync(query);

        // Assert
        bills.Count.Should().Be(1);
        bills.MetaData.HasNextPage.Should().BeFalse();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

}

