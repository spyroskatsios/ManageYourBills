using FluentAssertions;
using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.Enums;
using ManageYourBills.Domain.Exceptions.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ManageYourBills.Domain.Tests.Unit;
public class BillTests
{
    [Fact]
    public void CreateBill_ShouldThrowEmptyBillIdException_WhenIdIsEmpty()
    {
        // Arrange

        // Act
        Action result = () => new Bill(Guid.Empty, BillType.Electricity, new Provider(Guid.NewGuid(), "name"), new DateOnly(),
           new DateOnly(), new DateOnly(), new DateOnly(), string.Empty, 1, true, new DateOnly());

        // Assert
        result.Should().Throw<EmptyBillIdException>();
    }

    [Fact]
    public void CreateBill_ShouldThrowNullProviderException_WhenProviderIsNull()
    {
        // Arrange

        // Act
        Action result = () => new Bill(Guid.NewGuid(), BillType.Electricity, null, new DateOnly(),
            new DateOnly(), new DateOnly(), new DateOnly(), string.Empty, 1, true, new DateOnly());

        // Assert
        result.Should().Throw<NullProviderException>();
    }

    [Fact]
    public void CreateBill_ShouldThrowTooLongCommentsException_WhenCommentsTooLong()
    {
        // Arrange

        // Act
        Action result = () => new Bill(Guid.NewGuid(), BillType.Electricity, new Provider(Guid.NewGuid(), "name"), new DateOnly(),
            new DateOnly(), new DateOnly(), new DateOnly(), new string('a', 501), 1, true, new DateOnly());

        // Assert
        result.Should().Throw<TooLongCommentsException>();
    }


    [Fact]
    public void CreateBill_ShouldThrowInvalidDateException_WhenFromBiggerThanTo()
    {
        // Arrange

        // Act
        Action result = () => new Bill(Guid.NewGuid(), BillType.Electricity, new Provider(Guid.NewGuid(), "name"), new DateOnly(),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)), DateOnly.FromDateTime(DateTime.UtcNow), new DateOnly(),
            string.Empty, 1, true, new DateOnly());

        // Assert
        result.Should().Throw<InvalidDateException>();
    }

    [Fact]
    public void CreateBill_ShouldThrowInvalidDateException_WhenToBiggerThanExpiration()
    {
        // Arrange
        var a = new DateOnly();
        // Act
        Action result = () => new Bill(Guid.NewGuid(), BillType.Electricity, new Provider(Guid.NewGuid(), "name"), new DateOnly(),
            new DateOnly(), DateOnly.FromDateTime(DateTime.UtcNow).AddDays(1), DateOnly.FromDateTime(DateTime.UtcNow),
            string.Empty, 1, true, new DateOnly());

        // Assert
        result.Should().Throw<InvalidDateException>();
    }

    [Fact]
    public void CreateBill_ShouldThrowInvalidDateException_WhenIssueSmallerThanFrom()
    {
        // Arrange

        // Act
        Action result = () => new Bill(Guid.NewGuid(), BillType.Electricity, new Provider(Guid.NewGuid(), "name"),
            DateOnly.FromDateTime(DateTime.UtcNow),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)), new DateOnly(), DateOnly.FromDateTime(DateTime.UtcNow),
            string.Empty, 1, true, new DateOnly());

        // Assert
        result.Should().Throw<InvalidDateException>();
    }

    [Fact]
    public void CreateBill_ShouldThrowInvalidDateException_WhenIsPaisTrueAndPaidAtNull()
    {
        // Arrange

        // Act
        Action result = () => new Bill(Guid.NewGuid(), BillType.Electricity, new Provider(Guid.NewGuid(), "name"),
            DateOnly.FromDateTime(DateTime.UtcNow),
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)), new DateOnly(), DateOnly.FromDateTime(DateTime.UtcNow),
            string.Empty, 1, true, null);

        // Assert
        result.Should().Throw<InvalidDateException>();
    }
}
