using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.DbLocalizationProvider;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.Queries;

namespace FluiTec.AppFx.Localization.Handlers
{
    /// <summary>   A get all resources handler. </summary>
    public class GetAllResourcesHandler : IQueryHandler<GetAllResources.Query, IEnumerable<LocalizationResource>>
    {
        /// <summary>   The data service. </summary>
        private readonly ILocalizationDataService _dataService;

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        public GetAllResourcesHandler(ILocalizationDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
        }

        /// <summary>   Place where query handling happens. </summary>
        /// <param name="query">    This is the query instance. </param>
        /// <returns>
        ///     You have to return something from the query execution. Of course you can return
        ///     <c>null</c> as well if you will.
        /// </returns>
        public IEnumerable<LocalizationResource> Execute(GetAllResources.Query query)
        {
            var all = new List<LocalizationResource>();
            using (var uow = _dataService.BeginUnitOfWork())
            {
                var compound = uow.TranslationRepository.GetAllCompound();

                // ReSharper disable once IteratorMethodResultIsIgnored
                foreach (var entity in compound)
                {
                    var resource = new LocalizationResource(entity.Resource.ResourceKey)
                    {
                        Id = entity.Resource.Id,
                        Author = entity.Resource.Author,
                        FromCode = entity.Resource.FromCode,
                        IsHidden = entity.Resource.IsHidden,
                        IsModified = entity.Resource.IsModified,
                        ModificationDate = entity.Resource.ModificationDate
                    };
                    resource.Translations = new List<LocalizationResourceTranslation>(entity.Translations.Select(t =>
                        new LocalizationResourceTranslation
                        {
                            Id = t.Id,
                            Language = t.Language,
                            LocalizationResource = resource,
                            ResourceId = resource.Id,
                            Value = t.Value
                        }));
                    all.Add(resource);
                }
            }

            return all;
        }
    }
}