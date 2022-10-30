using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Minegame;
internal class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IGameManager, GameManager>();
                services.AddSingleton<IConfig, Config>();
                services.AddSingleton<IOutput, Output>();
            })
            .ConfigureAppConfiguration(app => app.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
            .Build();

        host.Services.GetRequiredService<IGameManager>().Start();

        await host.RunAsync();
    }
}