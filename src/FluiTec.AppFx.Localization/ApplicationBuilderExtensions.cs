using System;
using System.Diagnostics;
using System.Linq;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Entities;
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
            var synchronizer = new ResourceSynchronizer(builder.ApplicationServices.CreateScope().ServiceProvider.GetService<ILocalizationDataService>());
            synchronizer.DiscoverAndRegister();

            var resourceOptions = builder.ApplicationServices.GetService<LocalizationResourcesOptions>();
            if (resourceOptions != null && resourceOptions.Resources.Any())
                ImportJsonLocalizations(builder.ApplicationServices, resourceOptions);

            // in cases when there has been already a call to LoclaizationProvider.Current (some static weird things)
            // and only then setup configuration is ran - here we need to reset instance once again with new settings
            LocalizationProvider.Initialize();

            return builder;
        }

        /// <summary>   Import JSON localizations. </summary>
        /// <param name="serviceProvider">  The services to act on. </param>
        /// <param name="resourceOptions">  Options for controlling the resource. </param>
        private static void ImportJsonLocalizations(IServiceProvider serviceProvider, LocalizationResourcesOptions resourceOptions)
        {
            var resources = resourceOptions.Resources;
            using (var uow = serviceProvider.GetService<ILocalizationDataService>().BeginUnitOfWork())
            {
                foreach (var resource in resources)
                {
                    var r = uow.ResourceRepository.GetByKey(resource.ResourceKey) ?? uow.ResourceRepository.Add(
                        new ResourceEntity
                        {
                            Author = resource.Author,
                            FromCode = resource.FromCode,
                            IsHidden = resource.IsHidden,
                            IsModified = resource.IsModified,
                            ModificationDate = resource.ModificationDate,
                            ResourceKey = resource.ResourceKey
                        });

                    var existingTranslations = uow.TranslationRepository.ByResource(r).ToList();
                    foreach (var translation in resource.Translations)
                        if (!existingTranslations.Any(e => e.ResourceId == r.Id && e.Language == translation.Language))
                            uow.TranslationRepository.Add(new TranslationEntity
                            {
                                Language = translation.Language,
                                ResourceId = r.Id,
                                Value = translation.Value
                            });
                }

                uow.Commit();
            }
        }
    }
}