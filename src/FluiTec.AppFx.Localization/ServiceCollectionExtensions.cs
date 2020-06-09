using System;
using FluiTec.AppFx.Localization.Cache;
using FluiTec.AppFx.Localization.Handlers;
using FluiTec.DbLocalizationProvider;
using FluiTec.DbLocalizationProvider.Cache;
using FluiTec.DbLocalizationProvider.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization
{
    /// <summary>   A service collection extensions. </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     An IServiceCollection extension method that adds a database localization provider to
        ///     'setup'.
        /// </summary>
        /// <param name="services"> The services to act on. </param>
        /// <param name="setup">    (Optional) The setup. </param>
        /// <returns>   An IServiceCollection. </returns>
        public static IServiceCollection AddDbLocalizationProvider(this IServiceCollection services,
            Action<ConfigurationContext> setup = null)
        {
            // build serviceProvider to initialize localization
            var serviceProvider = services.BuildServiceProvider();

            ConfigurationContext.Current.TypeFactory.ForQuery<GetTranslation.Query>().SetHandler(() =>
                new GetTranslationHandler(serviceProvider.GetService<ILocalizationDataService>()));
            ConfigurationContext.Current.TypeFactory.ForQuery<GetAllResources.Query>().SetHandler(() =>
                new GetAllResourcesHandler(serviceProvider.GetService<ILocalizationDataService>()));
            ConfigurationContext.Current.TypeFactory.ForQuery<DetermineDefaultCulture.Query>()
                .SetHandler<DetermineDefaultCulture.Handler>();
            ConfigurationContext.Current.TypeFactory.ForCommand<ClearCache.Command>().SetHandler<ClearCacheHandler>();

            // check if there's a registered cache - if not - add one
            var cache = serviceProvider.GetService<IMemoryCache>();
            if (cache != null)
                ConfigurationContext.Current.CacheManager = new InMemoryCacheManager(cache);

            // run custom configuration setup (if any)
            setup?.Invoke(ConfigurationContext.Current);

            // setup model metadata providers
            if (ConfigurationContext.Current.ModelMetadataProviders.ReplaceProviders)
                services.Configure<MvcOptions>(_ =>
                {
                    _.ModelMetadataDetailsProviders.Add(new LocalizedDisplayMetadataProvider());
                    //_.ModelValidatorProviders.Add(new LocalizedValidationMetadataProvider());
                });

            services.AddSingleton<IStringLocalizerFactory, DbStringLocalizerFactory>();
            services.AddSingleton(_ => LocalizationProvider.Current);

            return services;
        }
    }
}