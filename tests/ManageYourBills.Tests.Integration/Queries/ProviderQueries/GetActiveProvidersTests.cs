using ManageYourBills.Application.Queries.ProviderQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration.Queries.ProviderQueries;

[Collection("TestBase")]
public class GetActiveProvidersTests
{
    private readonly TestBase _base;

    public GetActiveProvidersTests(TestBase @base)
    {
        _base = @base;
        _base.ResetState();
    }


    [Fact]
    public async Task GetActiveProviders_ShouldReturnActiveProviders()
    {
        // Arrange
        await _base.CreateProvidersAsync();
        var query = new GetActiveProvidersQuery();

        // Act
        var providers = await _base.SendAsync(query);

        // Assert
        providers.Count().Should().Be(2);

        foreach (var provider in providers)
        {
            provider.Archived.Should().BeFalse();
        }
    }
}
