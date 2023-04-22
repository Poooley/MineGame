using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

namespace Minegame;
internal class Program
{
    static async Task Main(string[] args) => await CreateDefaultHostBuilder(args).Build().RunAsync();
    private static IHostBuilder CreateDefaultHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
                    .ConfigureServices((context, services) =>
                    {
                        services.AddSingleton<IOutput, Output>();
                        services.Configure<Settings>(context.Configuration);
                        services.Configure<Settings>((settings) =>
                        {
                            settings.Fields = settings.Length * settings.Width;
                        });

                        services.AddHostedService<GameManager>();
                    });

    }
}