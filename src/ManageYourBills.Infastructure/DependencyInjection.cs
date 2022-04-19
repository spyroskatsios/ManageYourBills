using ManageYourBills.Application.Interfaces;
using ManageYourBills.Infastructure.Persistence;
using ManageYourBills.Infastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ManageYourBills.Infastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("SQLite"));
        });

        services.AddTransient<IAppDbContext, AppDbContext>();
        services.AddSingleton<ISortService, SortService>();
        services.AddTransient<IFileService, FileService>();

        return services;
    }
}
