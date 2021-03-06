﻿using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Localization.Cache;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Handlers;
using FluiTec.AppFx.Options.Managers;
using FluiTec.DbLocalizationProvider;
using FluiTec.DbLocalizationProvider.Cache;
using FluiTec.DbLocalizationProvider.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization
{
    /// <summary>   A service collection extensions. </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>   An IServiceCollection extension method that configure localization. </summary>
        /// <param name="services">             The services to act on. </param>
        /// <param name="configurationManager"> The setup. </param>
        /// <returns>   An IServiceCollection. </returns>
        public static IServiceCollection ConfigureLocalization(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var cultureOptions = services.Configure<CultureOptions>(configurationManager, true);
            
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = cultureOptions.SupportedCultures.Select(e => new CultureInfo(e)).ToList();
                options.DefaultRequestCulture = new RequestCulture(cultureOptions.DefaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            return services;
        }

        /// <summary>
        ///     An IServiceCollection extension method that adds a database localization provider to
        ///     'setup'.
        /// </summary>
        /// <param name="services">             The services to act on. </param>
        /// <param name="configurationManager"> The setup. </param>
        /// <returns>   An IServiceCollection. </returns>
        public static IServiceCollection AddDbLocalizationProvider(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            // resources found in json-config-files
            services.Configure<LocalizationResourcesOptions>(configurationManager);

            // build serviceProvider to initialize localization
            var serviceProvider = services.BuildServiceProvider();

            ConfigurationContext.Current.TypeFactory.ForQuery<GetTranslation.Query>().SetHandler(() =>
                new GetTranslationHandler(serviceProvider.GetService<ILocalizationDataService>()));
            ConfigurationContext.Current.TypeFactory.ForQuery<GetTranslation.MultiQuery>().SetHandler(() => 
                new GetTranslationsHandler(serviceProvider.GetService<ILocalizationDataService>()));
            ConfigurationContext.Current.TypeFactory.ForQuery<GetAllResources.Query>().SetHandler(() =>
                new GetAllResourcesHandler(serviceProvider.GetService<ILocalizationDataService>()));
            ConfigurationContext.Current.TypeFactory.ForQuery<DetermineDefaultCulture.Query>()
                .SetHandler<DetermineDefaultCulture.Handler>();
            ConfigurationContext.Current.TypeFactory.ForCommand<ClearCache.Command>().SetHandler<ClearCacheHandler>();

            // check if there's a registered cache - if not - add one
            var cache = serviceProvider.GetService<IMemoryCache>();
            if (cache != null)
                ConfigurationContext.Current.CacheManager = new InMemoryCacheManager(cache);

            // setup model metadata providers
            if (ConfigurationContext.Current.ModelMetadataProviders.ReplaceProviders)
                services.Configure<MvcOptions>(_ =>
                {
                    _.ModelMetadataDetailsProviders.Add(new LocalizedDisplayMetadataProvider());
                    //_.ModelValidatorProviders.Add(new LocalizedValidationMetadataProvider());
                });

            services.AddLocalization();
            // remove base implementation, because we'll provide one ourself
            services.Remove(services.Single(s => s.ServiceType == typeof(IStringLocalizer<>)));

            services.AddSingleton<IStringLocalizerFactory, DbStringLocalizerFactory>();
            services.AddSingleton(_ => LocalizationProvider.Current);
            services.AddTransient(typeof(IStringLocalizer<>), typeof(DbStringLocalizer<>));

            return services;
        }
    }
}