using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;

namespace Minegame;
internal class Program
{
    static void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true))
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<IGameManager, GameManager>();
                services.AddSingleton<IOutput, Output>();
                services.Configure<Settings>(context.Configuration);
                services.Configure<Settings>((settings) =>
                {
                    settings.Fields = settings.Length * settings.Width;
                });
            })
            .Build();

        host.Services.GetRequiredService<IGameManager>().Start();
    }
}