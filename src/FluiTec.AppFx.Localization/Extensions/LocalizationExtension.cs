using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Import;
using FluiTec.AppFx.Localization.Localizers;
using FluiTec.AppFx.Localization.Reflection.AssemblyScanner;
using FluiTec.AppFx.Localization.Reflection.Helpers;
using FluiTec.AppFx.Localization.Reflection.MemberScanner;
using FluiTec.AppFx.Localization.Reflection.TypeScanner;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.Extensions
{
    /// <summary>
    ///     A localization extension.
    /// </summary>
    public static class LocalizationExtension
    {
        /// <summary>
        ///     An IServiceCollection extension method that configure localization options.
        /// </summary>
        /// <param name="services"> The services to act on. </param>
        /// <param name="manager">  The manager. </param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        public static IServiceCollection ConfigureLocalizationOptions(this IServiceCollection services,
            ConfigurationManager manager)
        {
            services.Configure<ServiceLocalizationOptions>(manager, true);
            services.Configure<ServiceLocalizationImportOptions>(manager, true);

            return services;
        }

        /// <summary>
        ///     An IServiceCollection extension method that configure localization reflection.
        /// </summary>
        /// <param name="services"> The services to act on. </param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        public static IServiceCollection ConfigureLocalizationReflection(this IServiceCollection services)
        {
            services.AddSingleton<ReflectionHelper>();
            services.AddSingleton<IAssemblyScanner, ExclusionFilteringAssemblyScanner>();
            services.AddSingleton<ITypeScanner, LocalizedAttributeFilteringTypeScanner>();
            services.AddSingleton<IMemberScanner, DefaultFilteringMemberScanner>();

            return services;
        }

        /// <summary>
        ///     An IServiceCollection extension method that configure localizers.
        /// </summary>
        /// <param name="services"> The services to act on. </param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        public static IServiceCollection ConfigureLocalizers(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizer, ServiceStringLocalizer>();
            services.AddSingleton(typeof(IStringLocalizer<>), typeof(ServiceStringLocalizer<>));
            services.AddSingleton<IStringLocalizerFactory, ServiceStringLocalizerFactory>();

            return services;
        }

        /// <summary>
        ///     An IServiceCollection extension method that configure localization import.
        /// </summary>
        /// <param name="services"> The services to act on. </param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        public static IServiceCollection ConfigureLocalizationImport(this IServiceCollection services)
        {
            services.AddSingleton<ILocalizationSource, CodeLocalizationSource>();
            services.AddSingleton<ILocalizationSource, JsonFileLocalizationSource>();
            services.AddSingleton<ILocalizationImportService, LocalizationImportService>();

            return services;
        }
    }
}