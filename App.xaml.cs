using JogoGourmet.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.Windows;

namespace JogoGourmet;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _serviceProvider;

    public App()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<JogoGourmetContext>(options =>
            options.UseInMemoryDatabase("JogoGourmetDb"));
        services.AddSingleton<GuessEngine>();
        services.AddSingleton<StartWindow>();
        services.AddSingleton<SuccessWindow>();
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var startWindow = _serviceProvider.GetService<StartWindow>();
        startWindow.Show();
    }
}

