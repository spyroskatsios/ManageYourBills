using FluentAssertions;
using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.Exceptions.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ManageYourBills.Domain.Tests.Unit;
public class ProviderTests
{
    [Fact]
    public void CreateProvider_ShouldThrowEmptyOrNullProviderNameException_WhenNameIsNull()
    {
        // Arrange

        // Act
        Action result = () => new Provider(Guid.NewGuid(), null);

        // Assert
        result.Should().Throw<EmptyorNullProviderNameException>();
    }

    [Fact]
    public void CreateProvider_ShouldThrowEmptyOrNullProviderNameException_WhenNameIsEmpty()
    {
        // Arrange

        // Act
        Action result = () => new Provider(Guid.NewGuid(), string.Empty);

        // Assert
        result.Should().Throw<EmptyorNullProviderNameException>();
    }

    [Fact]
    public void CreateProvider_ShouldThrowEmptyProviderIdException_WhenIdIsEmpty()
    {
        // Arrange

        // Act
        Action result = () => new Provider(Guid.Empty, "name");

        // Assert
        result.Should().Throw<EmptyProviderIdException>();
    }

    [Fact]
    public void CreateProvider_ShouldThrowTooLongProviderNameExceptionn_WhenNameIsTooLong()
    {
        // Arrange

        // Act
        Action result = () => new Provider(Guid.NewGuid(), new string('a', 101));

        // Assert
        result.Should().Throw<TooLongProviderNameException>();
    }

    [Fact]
    public void CreateProvider_ShouldNotThrow_WhenAllPropertiesMeetCriteria()
    {
        // Arrange

        // Act
        Action result = () => new Provider(Guid.NewGuid(), "name");

        // Assert
        result.Should().NotThrow();
    }
}
