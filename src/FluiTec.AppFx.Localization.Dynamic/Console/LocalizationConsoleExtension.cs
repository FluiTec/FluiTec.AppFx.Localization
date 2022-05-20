using FluiTec.AppFx.Console;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Localization.Dynamic.Console
{
    /// <summary>
    /// A localization console extension.
    /// </summary>
    public static class LocalizationConsoleExtension
    {
        /// <summary>
        /// An IServiceCollection extension method that configure localization data module.
        /// </summary>
        ///
        /// <param name="services"> The services to act on. </param>
        public static void ConfigureLocalizationDataModule(this IServiceCollection services)
        {
            ConsoleHost.ConfigureModule(services, provider =>
            {
                return new LocalizationConsoleModule();
            });
        }
    }
}