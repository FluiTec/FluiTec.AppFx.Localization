using FluiTec.AppFx.Localization.Sync;
using FluiTec.DbLocalizationProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Localization
{
    /// <summary>   An application builder extensions. </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        ///     An IApplicationBuilder extension method that use database localization provider.
        /// </summary>
        /// <param name="builder">  The builder to act on. </param>
        /// <returns>   An IApplicationBuilder. </returns>
        public static IApplicationBuilder UseDbLocalizationProvider(this IApplicationBuilder builder)
        {
            var synchronizer =
                new ResourceSynchronizer(builder.ApplicationServices.GetService<ILocalizationDataService>());
            synchronizer.DiscoverAndRegister();

            // in cases when there has been already a call to LoclaizationProvider.Current (some static weird things)
            // and only then setup configuration is ran - here we need to reset instance once again with new settings
            LocalizationProvider.Initialize();

            return builder;
        }
    }
}