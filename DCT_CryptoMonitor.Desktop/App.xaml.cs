using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using DCT_CryptoMonitor.Core.Models;
using DCT_CryptoMonitor.Core.Services;
using DCT_CryptoMonitor.Desktop.MVVM.View;
using DCT_CryptoMonitor.Infrastructure.Configurations;
using DCT_CryptoMonitor.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace DCT_CryptoMonitor.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddSingleton(AddConfiguration());
        // services.Configure<ApiOptionsList>(c => c.ApiOptions = AddConfiguration().GetSection("Crypto").Get<Dictionary<string, ApiOptions>>()!);
        services.AddHttpClient();
        // services.AddSingleton<ICoinService, CoinGeckoClient>(c => new CoinGeckoClient(new HttpClient(), c.GetRequiredService<IConfiguration>().GetSection("Crypto:CoinGecko").Get<ApiOptions>()));
        services.AddSingleton<ICoinService, CoinCapClient>(c => new CoinCapClient(new HttpClient(), c.GetRequiredService<IConfiguration>().GetSection("Crypto:CoinCap").Get<ApiOptions>()));
        
        services.AddSingleton<MainWindow>();
        
        _serviceProvider = services.BuildServiceProvider();
    }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private IConfiguration AddConfiguration()
    {
        var builder = new ConfigurationBuilder();
        builder.AddJsonFile("appsettings.json", false, true);
        return builder.Build();
    }
}