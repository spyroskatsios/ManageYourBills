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
public class UpdateProviderTests
{
    private TestBase _base;

    public UpdateProviderTests(TestBase @base)
    {
        _base = @base;
        _base.ResetState();
    }

    [Fact]
    public async Task UpdateProvider_ShouldUpdateProvider()
    {
        // Arrange
        var provider = new Provider(Guid.NewGuid(), "Provider123");
        await _base.AddAsync(provider);
        var command = new UpdateProviderCommand(provider.Id, "UpdatedProvider");

        // Act
        await _base.SendAsync(command);

        // Assert
        var updatedProvider = await _base.FindAsync<Provider>(new ProviderId(provider.Id));

        updatedProvider.Should().NotBeNull();
        updatedProvider.Name.Value.Should().Be(command.Name);
    }

    [Fact]
    public async Task ChangeProviderStatus_ShouldChangeProviderStatus()
    {
        // Arrange
        var provider = new Provider(Guid.NewGuid(), "Provider123");
        var initialStatus = provider.Archived;
        await _base.AddAsync(provider);
        var command = new ChangeProviderStatusCommand(provider.Id);

        // Act
        await _base.SendAsync(command);

        // Assert
        provider = await _base.FindAsync<Provider>(new ProviderId(provider.Id));
        provider.Archived.Should().Be(!initialStatus);
    }
}
