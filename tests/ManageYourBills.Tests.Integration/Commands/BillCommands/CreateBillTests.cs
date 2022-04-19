using ManageYourBills.Application.Commands.BillCommands;
using ManageYourBills.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration.Commands.BillCommands;

[Collection("TestBase")]
public class CreateBillTests
{
    private readonly TestBase _base;
    private DateOnly issued;
    private DateOnly from;
    private DateOnly to;
    private DateOnly? paidAt;
    private DateOnly expires;

    public CreateBillTests(TestBase @base)
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
    public async Task CreateBill_ShouldCreateBill()
    {
        // Arrange
        var providers = await _base.CreateProvidersAsync();
        var command = new CreateBillCommand(BillType.Mobile.ToString(), providers[0].Id.Value, issued, from, to, expires,
            string.Empty, 25, true, paidAt);

        // Act
        var id = await _base.SendAsync(command);

        // Assert
        var bill = await _base.FindBillWithProviderAsync(id);

        bill.Should().NotBeNull();
        bill.Id.Value.Should().Be(id);
        bill.Provider.Id.Value.Should().Be(command.ProviderId);
        bill.Issued.Should().Be(issued);
        bill.From.Should().Be(from);
        bill.To.Should().Be(to);
        bill.Expiration.Should().Be(expires);
        bill.PaidAt.Should().Be(paidAt);
        bill.Comments.Should().Be(command.Comments);
        bill.Cost.Should().Be(command.Cost);
        bill.IsPaid.Should().Be(command.IsPaid);
    }
}
