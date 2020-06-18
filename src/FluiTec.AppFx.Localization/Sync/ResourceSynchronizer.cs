using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.DbLocalizationProvider;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.Cache;
using FluiTec.DbLocalizationProvider.Internal;
using FluiTec.DbLocalizationProvider.Queries;
using FluiTec.DbLocalizationProvider.Sync;

namespace FluiTec.AppFx.Localization.Sync
{
    /// <summary>   A resource synchronizer. </summary>
    public class ResourceSynchronizer
    {
        /// <summary>   The data service. </summary>
        private readonly ILocalizationDataService _dataService;

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        public ResourceSynchronizer(ILocalizationDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        /// <summary>   Discover and register. </summary>
        public void DiscoverAndRegister()
        {
            if (!ConfigurationContext.Current.DiscoverAndRegisterResources)
                return;

            var discoveredTypes = TypeDiscoveryHelper.GetTypes(
                t => t.GetCustomAttribute<LocalizedResourceAttribute>() != null,
                t => t.GetCustomAttribute<LocalizedModelAttribute>() != null);

            var discoveredResources = discoveredTypes[0];
            var discoveredModels = discoveredTypes[1];
            var foreignResources = ConfigurationContext.Current.ForeignResources;
            if (foreignResources != null && foreignResources.Any())
                discoveredResources.AddRange(foreignResources.Select(x => x.ResourceType));

            ResetSyncStatus();
            var allResources = new GetAllResources.Query().Execute().ToList();

            RegisterDiscoveredResources(discoveredResources, allResources);
            RegisterDiscoveredResources(discoveredModels, allResources);

            if (ConfigurationContext.Current.PopulateCacheOnStartup)
                PopulateCache();
        }

        /// <summary>   Resets the synchronise status. </summary>
        private void ResetSyncStatus()
        {
            using (var uow = _dataService.BeginUnitOfWork())
            {
                uow.ResourceRepository.ResetSyncStatus();
                uow.Commit();
            }
        }

        /// <summary>   Populate cache. </summary>
        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void PopulateCache()
        {
            new ClearCache.Command().Execute();
            var allResources = new GetAllResources.Query().Execute();

            foreach (var resource in allResources)
            {
                var key = CacheKeyHelper.BuildKey(resource.ResourceKey);
                ConfigurationContext.Current.CacheManager.Insert(key, resource);
            }
        }

        /// <summary>Registers the discovered resources.</summary>
        /// <param name="types">        The types. </param>
        /// <param name="allResources"> all resources. </param>
        private void RegisterDiscoveredResources(IEnumerable<Type> types, IList<LocalizationResource> allResources)
        {
            var helper = new TypeDiscoveryHelper();
            var props = types.SelectMany(type => helper.ScanResources(type)).DistinctBy(r => r.Key);

            // split work queue by 400 resources each
            var groupedProperties = props.SplitByCount(400);

            // loop through all groups
            foreach (var group in groupedProperties)
            {
                using (var uow = _dataService.BeginUnitOfWork())
                {
                    var resourceRepository = uow.ResourceRepository;
                    var translationRepository = uow.TranslationRepository;

                    // enumerate beforehand
                    var properties = @group.ToList();

                    // loop through all refactored properties and change their key
                    foreach (var refactoredResource in properties.Where(r => !string.IsNullOrEmpty(r.OldResourceKey)))
                        if (resourceRepository.RefactorKey(refactoredResource.OldResourceKey, refactoredResource.Key))
                            allResources.Single(r => r.ResourceKey == refactoredResource.OldResourceKey).ResourceKey =
                                refactoredResource.Key;

                    // loop through all properties
                    foreach (var property in properties)
                    {
                        var existingResource = allResources.FirstOrDefault(r => r.ResourceKey == property.Key);
                        if (existingResource == null)
                        {
                            // add the new resource
                            var newResource = resourceRepository.Add(new ResourceEntity
                            {
                                Author = "Code",
                                FromCode = true,
                                IsHidden = property.IsHidden,
                                IsModified = false,
                                ResourceKey = property.Key,
                                ModificationDate = DateTime.Now
                            });

                            //we don't have to respect existing translations - just insert them all
                            translationRepository.AddRange(property.Translations.Select(t => new TranslationEntity
                            {
                                ResourceId = newResource.Id,
                                Language = t.Culture,
                                Value = t.Translation
                            }));
                        }
                        else
                        {
                            // update the existing resource
                            var resourceEntity = resourceRepository.GetByKey(existingResource.ResourceKey);
                            resourceEntity.FromCode = true;
                            resourceEntity.IsHidden = property.IsHidden;
                            resourceRepository.Update(resourceEntity);

                            // add/update languages
                            var byResource = translationRepository.ByResource(resourceEntity);
                            var existingTranslations = byResource.ToDictionary(t => t.Language ?? string.Empty);
                            foreach (var translation in property.Translations)
                                // if it exists - update it if it was modified or the invariant translations
                                if (existingTranslations.ContainsKey(translation.Culture))
                                {
                                    // skip if modified - or culture isnt invariant
                                    if (translation.Culture != string.Empty &&
                                        (!existingResource.IsModified.HasValue || existingResource.IsModified.Value))
                                        continue;

                                    var entity = existingTranslations[translation.Culture];
                                    entity.Value = translation.Translation;
                                    translationRepository.Update(entity);
                                }
                                // if it doesnt - add it
                                else
                                {
                                    translationRepository.Add(new TranslationEntity
                                    {
                                        Language = translation.Culture,
                                        ResourceId = existingResource.Id,
                                        Value = translation.Translation
                                    });
                                }
                        }
                    }

                    uow.Commit();
                }
            }
        }
    }
}