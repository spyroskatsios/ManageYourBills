using ManageYourBills.Application.Queries.BillQueries;
using ManageYourBills.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration.Queries.BillQueries;

[Collection("TestBase")]
public class GetBillByIdTests
{
    private readonly TestBase _base;

    private DateOnly issued;
    private DateOnly from;
    private DateOnly to;
    private DateOnly? paidAt;
    private DateOnly expires;

    public GetBillByIdTests(TestBase @base)
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
    public async Task GetBillById_ShouldReturnBill()
    {
        // Arrange
        var providerId = Guid.NewGuid();
        var bill = new Bill(Guid.NewGuid(), BillType.Tv, new Provider(providerId, "111"), issued, from, to, expires,
          string.Empty, 25, true, paidAt);
        await _base.AddAsync(bill);
        var query = new GetBillByIdQuery(bill.Id);

        // Act
        var response = await _base.SendAsync(query);

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(bill.Id);
        response.Provider.Id.Should().Be(providerId);
        response.Provider.Name.Should().Be("111");
        response.Issued.Should().Be(issued);
        response.From.Should().Be(from);
        response.To.Should().Be(to);
        response.Expiration.Should().Be(expires);
        response.PaidAt.Should().Be(paidAt);
        response.Comments.Should().Be(bill.Comments);
        response.Cost.Should().Be(bill.Cost);
        response.IsPaid.Should().Be(bill.IsPaid);
    }
}
