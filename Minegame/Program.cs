using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

namespace Minegame;
internal class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IGameManager, GameManager>();
                services.AddSingleton<IOutput, Output>();
                services.Configure<MySettings>(context.Configuration);
                services.Configure<MySettings>((settings) =>
                {
                    settings.Fields = settings.Length * settings.Width;
                });
            })
            .Build();

        host.Services.GetRequiredService<IGameManager>().Start();
    }
}