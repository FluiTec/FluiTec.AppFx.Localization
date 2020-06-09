using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.DbLocalizationProvider;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.Cache;
using FluiTec.DbLocalizationProvider.Queries;

namespace FluiTec.AppFx.Localization.Handlers
{
    /// <summary>   A get translation handler. </summary>
    public class GetTranslationHandler : GetTranslation.GetTranslationHandlerBase,
        IQueryHandler<GetTranslation.Query, string>
    {
        /// <summary>   The data service. </summary>
        private readonly ILocalizationDataService _dataService;

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        public GetTranslationHandler(ILocalizationDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        /// <summary>   Place where query handling happens. </summary>
        /// <param name="query">    This is the query instance. </param>
        /// <returns>
        ///     You have to return something from the query execution. Of course you can return
        ///     <c>null</c> as well if you will.
        /// </returns>
        public string Execute(GetTranslation.Query query)
        {
            if (!ConfigurationContext.Current.EnableLocalization())
                return query.Key;

            var key = query.Key;
            var language = query.Language;
            var cacheKey = CacheKeyHelper.BuildKey(key);

            if (ConfigurationContext.Current.CacheManager.Get(cacheKey) is LocalizationResource localizationResource)
                return GetTranslationFromAvailableList(localizationResource.Translations, language, query.UseFallback)
                    ?.Value;

            var resource = GetResourceFromDb(key);
            LocalizationResourceTranslation localization = null;

            if (resource == null)
                resource = LocalizationResource.CreateNonExisting(key);
            else
                localization = GetTranslationFromAvailableList(resource.Translations, language, query.UseFallback);

            ConfigurationContext.Current.CacheManager.Insert(cacheKey, resource);
            return localization?.Value;
        }

        /// <summary>   Gets resource from database. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>   The resource from database. </returns>
        private LocalizationResource GetResourceFromDb(string key)
        {
            using (var uow = _dataService.BeginUnitOfWork())
            {
                var resource = uow.ResourceRepository.GetByKey(key);
                if (resource == null) return null;

                var translations = uow.TranslationRepository.ByResource(resource);
                if (translations == null)
                    return new LocalizationResource(key) {Translations = new List<LocalizationResourceTranslation>()};

                var r = new LocalizationResource(key)
                {
                    Id = resource.Id,
                    Author = resource.Author,
                    FromCode = resource.FromCode,
                    IsHidden = resource.IsHidden,
                    IsModified = resource.IsModified,
                    ModificationDate = resource.ModificationDate
                };
                r.Translations = translations.Select(t => new LocalizationResourceTranslation
                {
                    Id = t.Id,
                    Language = t.Language,
                    ResourceId = t.ResourceId,
                    LocalizationResource = r,
                    Value = t.Value
                }).ToList();
                return r;
            }
        }
    }
}