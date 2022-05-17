using FluiTec.AppFx.Localization.Extensions;
using FluiTec.AppFx.Localization.Services;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace FluiTec.AppFx.Localization.Dynamic.Extensions
{
    /// <summary>
    ///     A localization extension.
    /// </summary>
    public static class LocalizationExtension
    {
        /// <summary>
        ///     An IServiceCollection extension method that configure localization services.
        /// </summary>
        /// <param name="services"> The services to act on. </param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        public static IServiceCollection ConfigureLocalizationServices(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));
            services.AddSingleton<ITranslationPickingService, ParentTranslationPickingService>();
            services.AddSingleton(typeof(ILocalizationService), typeof(MemoryBackedDataLocalizationService));

            return services;
        }

        /// <summary>
        ///     An IServiceCollection extension method that configure localization.
        /// </summary>
        /// <param name="services"> The services to act on. </param>
        /// <param name="manager">  The manager. </param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        public static IServiceCollection ConfigureLocalization(this IServiceCollection services,
            ConfigurationManager manager)
        {
            services.ConfigureDynamicLocalizationDataProvider(manager);
            services.ConfigureLocalizationOptions(manager);
            services.ConfigureLocalizationServices();
            services.ConfigureLocalizers();
            services.ConfigureLocalizationReflection();
            services.ConfigureLocalizationImport();

            return services;
        }
    }
}