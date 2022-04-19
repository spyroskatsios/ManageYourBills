using ManageYourBills.Application.Queries.ProviderQueries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration.Queries.ProviderQueries;

[Collection("TestBase")]
public class GetAllProvidersTests
{
    private readonly TestBase _base;

    public GetAllProvidersTests(TestBase @base)
    {
        _base = @base;
        _base.ResetState();
    }


    [Fact]
    public async Task GetAllProviders_ShouldReturnAllProviders()
    {
        // Arrange
        await _base.CreateProvidersAsync();
        var query = new GetAllProvidersQuery();

        // Act
        var providers = await _base.SendAsync(query);

        // Assert
        providers.Count().Should().Be(4);
    }
}
