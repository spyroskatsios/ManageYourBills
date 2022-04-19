using ManageYourBills.BlazorServer;
using ManageYourBills.Infastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Tests.Integration;
public class TestBase
{
    private IConfiguration _configuration = null!;
    private IServiceScopeFactory _scopeFactory = null!;

    public TestBase()
    {
        var a = Directory.GetCurrentDirectory();
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

        _configuration = builder.Build();

        var startup = new Startup(_configuration);

        var services = new ServiceCollection();

        var webHostEnviroment = Substitute.For<IWebHostEnvironment>();
        webHostEnviroment.EnvironmentName.Returns("Development");
        webHostEnviroment.ApplicationName.Returns("ManageYourBills.BlazorServer");

        services.AddSingleton(webHostEnviroment);

        startup.ConfigureServices(services);

        _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

        EnsureDatabase();
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    private void EnsureDatabase()
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Database.Migrate();
    }

    public void ResetState()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var providers = context.Providers.ToList();
        context.Providers.RemoveRange(providers);
        context.SaveChanges();
    }

    public async Task<TEntity?> FindAsync<TEntity>(object keyValues) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Set<TEntity>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public async Task<Bill> FindBillWithProviderAsync(BillId id)
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        return await context.Bills.AsNoTracking().Include(x => x.Provider).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Provider>> CreateProvidersAsync()
    {
        var providers = new List<Provider>()
        {
            new Provider(Guid.NewGuid(), "1"),
            new Provider(Guid.NewGuid(), "2"),
            new Provider(Guid.NewGuid(), "3"),
            new Provider(Guid.NewGuid(), "4"),
        };

        providers[1].Archived = true;
        providers[3].Archived = true;

        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Providers.AddRangeAsync(providers);
        await context.SaveChangesAsync();
        return providers;
    }

    public async Task CreateBillWithExistingProviderAsync(Bill bill)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var provider = await context.Providers.FindAsync(new ProviderId(bill.Provider.Id));
        bill.Provider = provider;
        await context.Bills.AddAsync(bill);
        await context.SaveAsync();
    }
}
