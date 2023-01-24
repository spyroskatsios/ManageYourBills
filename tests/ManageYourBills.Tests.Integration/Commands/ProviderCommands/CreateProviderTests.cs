using FluentAssertions;
using ManageYourBills.Application.Commands.ProviderCommands;
using ManageYourBills.Domain.Entities;
using ManageYourBills.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ManageYourBills.Tests.Integration.Commands.ProviderCommands;

[Collection("TestBase")]
public class CreateProviderTests
{
    private readonly TestBase _base;

    public CreateProviderTests(TestBase @base)
    {
        _base = @base;
        _base.ResetState();
    }

    [Fact]
    public async Task CreateProvider_ShouldCreateProvider()
    {
        // Arrange
        var command = new CreateProviderCommand("Provider123");

        // Act
        var id = await _base.SendAsync(command);

        // Assert
        var provider = await _base.FindAsync<Provider>(new ProviderId(id));

        provider.Should().NotBeNull();
        provider.Name.Value.Should().Be(command.Name);
    }
}
