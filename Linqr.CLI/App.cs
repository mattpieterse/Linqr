using JetBrains.Annotations;
using Linqr.CLI.Core.Commands;
using Linqr.CLI.Core.Injection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Net.Codecrete.QrCodeGenerator;
using Serilog;
using Spectre.Console.Cli;

namespace Linqr.CLI;

public static class App
{
#region Lifecycle

    /// <summary>
    /// The main entry-point for the application.
    /// </summary>
    /// <param name="arguments">
    /// Arguments parsed from the command-line upon execution of the application
    /// via a terminal. Used by the <see cref="Spectre.Console.Cli.CommandApp"/>
    /// to execute commands with arguments.
    /// </param>
    /// <returns>
    /// <see cref="Environment.ExitCode"/>
    /// </returns>
    public static void Main(
        string[] arguments
    ) {
        var configuration = BuildConfigurations();
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        try {
            Log.Information("Application started");

            var builder = Host.CreateDefaultBuilder(arguments);
            builder
                .UseSerilog()
                .ConfigureServices((_, services) => {
                    services.AddLogging(); //
                });

            var app = builder.BuildApplication();

            app.Configure(options => {
                options.SetApplicationName("linqr");
                options.UseAssemblyInformationalVersion();
                options.Settings.TrimTrailingPeriod = false;

                // Commands

                options.AddCommand<EncodeCommand>("encode");
            });

            app.Run(arguments);

            #if DEBUG
            Console.ReadLine();
            #endif
        }
        catch (Exception ex) {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally {
            Log.Information("Application closing");
            Log.CloseAndFlush();
        }
    }

#endregion

#region Internals

    /// <summary>
    /// TODO
    /// </summary>
    private static IConfiguration BuildConfigurations() {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("logging.json", optional: false, reloadOnChange: true)
            .AddJsonFile("options.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }

#endregion
}
