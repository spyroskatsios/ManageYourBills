using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration.Commands.ProviderCommands;

[Collection("TestBase")]
public class DeleteProviderTests
{
    private readonly TestBase _base;

    public DeleteProviderTests(TestBase @base)
    {
        _base = @base;
    }

    [Fact]
    public async Task DeleteProvider_ShouldDeleteProvider()
    {
        // Arrange
        var provider = new Provider(Guid.NewGuid(), "Provider123");
        await _base.AddAsync(provider);
        var command = new DeleteProviderCommand(provider.Id);

        // Act
        await _base.SendAsync(command);

        // Assert
        provider = await _base.FindAsync<Provider>(new ProviderId(provider.Id));
        provider.Should().BeNull();
    }
}
