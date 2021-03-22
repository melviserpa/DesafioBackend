using System;
using System.IO;
using System.Reflection;

using Investimentos.API.CrossCutting.Config;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Serilog;

namespace Investimentos.API
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            var appName = Assembly.GetCallingAssembly().GetName().Name;
            var appVersion = Assembly.GetCallingAssembly().GetName().Version.ToString();

            Console.Title = $"{appName} v{appVersion}";

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", appName)
                .Enrich.WithProperty("Version", appVersion)
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            try
            {
                Log.Warning("Application {0} start", appName);
                Log.Warning("Version {0}", appVersion);
                Log.Information("Logging level set to {0}", Configuration.GetSection(Constantes.Serilog.MinimumLevel).Value);
                Log.Information("Load configurations from {0}", Configuration.GetSection(Constantes.AppConfig.Leitura).Value);

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config
                        .AddConfiguration(Configuration)
                        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
                }).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .UseKestrel(options =>
                    {
                        options.AddServerHeader = false;
                    })
                    .UseSerilog()
                    .UseStartup<Startup>();
                });
    }
}
