using ManageYourBills.Application.Commands.BillCommands;
using ManageYourBills.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration.Commands.BillCommands;

[Collection("TestBase")]
public class DeleteBillTests
{
    private readonly TestBase _base;
    private DateOnly issued;
    private DateOnly from;
    private DateOnly to;
    private DateOnly? paidAt;
    private DateOnly expires;

    public DeleteBillTests(TestBase @base)
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
    public async Task DeleteBill_ShouldDeleteBill()
    {
        // Arrange
        //var provider = await _base.FindAsync<Provider>(new ProviderId((await _base.CreateProvidersAsync())[0]));
        var bill = new Bill(Guid.NewGuid(), BillType.Tv, new Provider(Guid.NewGuid(), "111"), issued, from, to, expires,
            string.Empty, 25, true, paidAt);
        await _base.AddAsync(bill);
        var command = new DeleteBillCommand(bill.Id);

        // Act
        await _base.SendAsync(command);

        // Assert
        bill = await _base.FindAsync<Bill>(new BillId(bill.Id));

        bill.Should().BeNull();
    }
}
