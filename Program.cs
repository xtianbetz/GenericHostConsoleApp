using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace GenericHostConsoleApp
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddSystemdConsole(options =>
                    {
                        options.TimestampFormat = "hh:mm:ss.fff ";
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddHostedService<ConsoleHostedService>()
                        .AddSingleton<IWeatherService, WeatherService>();

                    services.AddOptions<WeatherSettings>().Bind(hostContext.Configuration.GetSection("Weather"));
                })
                .RunConsoleAsync();
        }
    }
}
