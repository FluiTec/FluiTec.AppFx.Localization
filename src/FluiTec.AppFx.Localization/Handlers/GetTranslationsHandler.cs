using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.DbLocalizationProvider;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.Queries;

namespace FluiTec.AppFx.Localization.Handlers
{
    public class GetTranslationsHandler : GetTranslation.GetTranslationHandlerBase,
        IQueryHandler<GetTranslation.MultiQuery, IEnumerable<KeyValuePair<string, string>>>
    {
        /// <summary>   The data service. </summary>
        private readonly ILocalizationDataService _dataService;

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        public GetTranslationsHandler(ILocalizationDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        /// <summary>   Place where query handling happens. </summary>
        /// <param name="query">    This is the query instance. </param>
        /// <returns>
        ///     You have to return something from the query execution. Of course you can return
        ///     <c>null</c> as well if you will.
        /// </returns>
        public IEnumerable<KeyValuePair<string, string>> Execute(GetTranslation.MultiQuery query)
        {
            var tmp = new List<KeyValuePair<string, string>>();
            var r = GetResourcesFromDb(query.Key);
            foreach(var entry in r)
            {
                tmp.Add(new KeyValuePair<string, string>(entry.ResourceKey, GetTranslationFromAvailableList(entry.Translations, query.Language, query.UseFallback)?.Value));
            }
            return tmp;
        }

        /// <summary>   Gets resource from database. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>   The resource from database. </returns>
        private IEnumerable<LocalizationResource> GetResourcesFromDb(string key)
        {
            using (var uow = _dataService.BeginUnitOfWork())
            {
                var resources = uow.ResourceRepository.GetByKeyBeginsWith(key);
                if (!resources.Any()) return null;
                
                var translations = resources.Select(r => new Tuple<ResourceEntity, IEnumerable<TranslationEntity>>(r, uow.TranslationRepository.ByResource(r).ToList())).ToList();

                if (translations == null)
                    return resources.Select(r => new LocalizationResource(r.ResourceKey) { Translations = new List<LocalizationResourceTranslation>() });

                return resources.Select(r => new LocalizationResource(r.ResourceKey) 
                {
                    Id = r.Id,
                    Author = r.Author,
                    FromCode = r.FromCode,
                    IsHidden = r.IsHidden,
                    IsModified = r.IsModified,
                    ModificationDate = r.ModificationDate,
                    Translations = translations.SingleOrDefault(t => t.Item1 == r)?.Item2?.Select(t => new LocalizationResourceTranslation() 
                    {
                        Id = t.Id,
                        Language = t.Language,
                        ResourceId = t.ResourceId,
                        //LocalizationResource = r,
                        Value = t.Value
                    }).ToList()
                });
            }
        }
    }
}