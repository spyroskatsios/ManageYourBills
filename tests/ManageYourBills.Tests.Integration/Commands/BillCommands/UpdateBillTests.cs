using ManageYourBills.Application.Commands.BillCommands;
using ManageYourBills.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration.Commands.BillCommands;

[Collection("TestBase")]
public class UpdateBillTests
{
    private readonly TestBase _base;

    private DateOnly issued;
    private DateOnly from;
    private DateOnly to;
    private DateOnly? paidAt;
    private DateOnly expires;

    public UpdateBillTests(TestBase @base)
    {
        _base = @base;
        _base.ResetState();

        issued = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5));
        from = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-40));
        to = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-10));
        expires = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(20));
        paidAt = DateOnly.FromDateTime(DateTime.UtcNow);
    }

    [Fact]
    public async Task UpdateBill_ShouldUpdateBill()
    {
        // Arrange
        var providers = await _base.CreateProvidersAsync();
        var provider = await _base.FindAsync<Provider>(new ProviderId(providers[0].Id.Value));
        var bill = new Bill(Guid.NewGuid(), BillType.Tv, new Provider(Guid.NewGuid(), "123"), issued, from, to, expires,
            string.Empty, 25, false, null);
        await _base.AddAsync(bill);
        var command = new UpdateBillCommand(bill.Id, BillType.Water.ToString(), providers[1].Id.Value, issued.AddDays(1),
            from.AddDays(1), to.AddDays(1), expires.AddDays(1), "aa", 30, true, paidAt);

        // Act
        await _base.SendAsync(command);

        // Assert
        bill = await _base.FindBillWithProviderAsync(bill.Id);

        bill.Should().NotBeNull();
        bill.Id.Value.Should().Be(command.Id);
        bill.Provider.Id.Value.Should().Be(command.ProviderId);
        bill.Issued.Should().Be(command.Issued);
        bill.From.Should().Be(command.From);
        bill.To.Should().Be(command.To);
        bill.Expiration.Should().Be(command.Expiration);
        bill.PaidAt.Should().Be(command.PaidAt);
        bill.Comments.Should().Be(command.Comments);
        bill.Cost.Should().Be(command.Cost);
        bill.IsPaid.Should().Be(command.IsPaid);

    }
}
