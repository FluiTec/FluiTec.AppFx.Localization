using FluiTec.AppFx.Console;
using FluiTec.AppFx.Console.Configuration;
using FluiTec.AppFx.Data.Dynamic.Console;
using FluiTec.AppFx.Localization.Dynamic;
using FluiTec.AppFx.Localization.Dynamic.Console;
using FluiTec.AppFx.Options.Console;
using FluiTec.AppFx.Options.Programs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cli.InteractiveSample;


/// <summary>
///     A program.
/// </summary>
internal class Program : ValidatingConfigurationManagerProgram
{
    /// <summary>
    ///     Main entry-point for this application.
    /// </summary>
    /// <param name="args"> An array of command-line argument strings. </param>
    private static void Main(string[] args)
    {
        var sp = new Program().GetServiceProvider();
        new ConsoleHost(sp).RunInteractive("Test", args);
    }

    
    /// <summary>
    ///     Configures the given configuration builder.
    /// </summary>
    /// <param name="configurationBuilder"> The configuration builder. </param>
    /// <returns>
    ///     An IConfigurationBuilder.
    /// </returns>
    protected override IConfigurationBuilder Configure(IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("appsettings.secret.json", false, true)
            .AddSaveableJsonFile("appsettings.conf.json", false, true);
    }

    /// <summary>
    ///     Configure services.
    /// </summary>
    /// <param name="services"> The services. </param>
    /// <returns>
    ///     A ServiceCollection.
    /// </returns>
    protected override ServiceCollection ConfigureServices(ServiceCollection services)
    {
        base.ConfigureServices(services);

        services.ConfigureDynamicLocalizationDataProvider(Manager);

        services.ConfigureOptionsConsoleModule();
        services.ConfigureDataConsoleModule();
        services.ConfigureLocalizationDataModule();

        return services;
    }
}