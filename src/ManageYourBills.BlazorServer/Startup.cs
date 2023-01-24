using ElectronNET.API;
using ElectronNET.API.Entities;
using ManageYourBills.Application;
using ManageYourBills.BlazorServer.Services;
using ManageYourBills.Infrastructure;
using MudBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.BlazorServer;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();

        services.AddMudServices();

        services.AddApplication();
        services.AddInfastructure(Configuration);

        services.AddSingleton<IMapper, Mapper>();
        services.AddTransient<IDispatcher, Dispatcher>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });

        if (HybridSupport.IsElectronActive)
        {
            Task.Run(async () => await ElectronInit());
        }
    }

    private static async Task ElectronInit()
    {
        var windowOptions = new BrowserWindowOptions
        {
            MinWidth = 1200,
            MinHeight = 700,
            Title = "Manage Your Bills,",
            DarkTheme = true,
            Height = 1000,
            Width = 1200,
            Icon = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/bill_icon.png")
        };
        
        var window = await Electron.WindowManager.CreateWindowAsync(windowOptions);
        window.SetTitle("Manage Your Bills");
        
        window.OnReadyToShow += () => window.Show();
        window.OnClosed += ()=> Electron.App.Quit();
    }
}
