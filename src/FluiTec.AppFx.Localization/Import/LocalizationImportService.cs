using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    /// A service for accessing localization imports information.
    /// </summary>
    public class LocalizationImportService : ILocalizationImportService
    {
        /// <summary>
        /// Gets the data service.
        /// </summary>
        ///
        /// <value>
        /// The data service.
        /// </value>
        public ILocalizationDataService DataService { get; }

        /// <summary>
        /// Gets options for controlling the operation.
        /// </summary>
        ///
        /// <value>
        /// The options.
        /// </value>
        public ServiceLocalizationImportOptions Options { get; }

        /// <summary>
        /// Gets the localization sources.
        /// </summary>
        ///
        /// <value>
        /// The localization sources.
        /// </value>
        public IEnumerable<ILocalizationSource> LocalizationSources { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dataService">          The data service. </param>
        /// <param name="options">              The options. </param>
        /// <param name="localizationSources">  The localization sources. </param>
        public LocalizationImportService(ILocalizationDataService dataService, ServiceLocalizationImportOptions options, 
            IEnumerable<ILocalizationSource> localizationSources)
        {
            DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            Options = options ?? throw new ArgumentNullException(nameof(options));
            LocalizationSources = localizationSources ?? throw new ArgumentNullException(nameof(localizationSources));
        }

        /// <summary>
        /// Gets the import.
        /// </summary>
        ///
        /// <returns>
        /// An int.
        /// </returns>
        public int Import()
        {
            var changeCounter = 0;

            var resources = new List<ILocalizationResource>();
            foreach (var source in LocalizationSources)
            {
                resources.AddRange(source.FindResources().ToList());
            }

            using var uow = DataService.BeginUnitOfWork();
            var languages = EnsureLanguages(uow, resources.Select(r => r.Language).Distinct());
            var authors = EnsureAuthors(uow, resources.Select(r => r.Author).Distinct());

            foreach (var group in resources.GroupBy(r => r.ResourceKey))
            {
                var author = authors.Single(a => a.Name == group.First().Author);

                var resource = uow.ResourceRepository.Get(group.Key);
                resource ??= uow.ResourceRepository.Add(new ResourceEntity
                {
                    ResourceKey = group.Key,
                    AuthorId = author.Id,
                    ModificationDate = DateTimeOffset.UtcNow
                });

                var translations = uow.TranslationRepository.GetByResource(group.Key).ToList();
                foreach (var entry in group)
                {
                    var language = languages.Single(l => l.IsoName == entry.Language);
                    if (translations.All(t => languages.Single(l => l.Id == t.LanguageId).IsoName != entry.Language))
                    {
                        // doesnt exist -> create it
                        translations.Add(uow.TranslationRepository.Add(new TranslationEntity
                        {
                            AuthorId = author.Id, 
                            LanguageId = language.Id,
                            ResourceId = resource.Id,
                            ModificationDate = DateTimeOffset.UtcNow,
                            Value = entry.Translation
                        }));
                        changeCounter++;
                    }
                    else
                    {
                        // check if an update is allowed and necessary
                        var translation = translations.Single(t =>
                            languages.Single(l => l.Id == t.LanguageId).IsoName == entry.Language);

                        if (!Options.UpdateableAuthors.Contains(authors.Single(a => a.Id == translation.AuthorId)
                                .Name) || translation.Value == entry.Translation) continue;
                        translation.Value = entry.Translation;
                        translation.ModificationDate = DateTimeOffset.UtcNow;
                        uow.TranslationRepository.Update(translation);
                        changeCounter++;
                    }
                }
            }

            uow.Commit();

            return changeCounter;
        }

        /// <summary>
        /// Ensures that languages.
        /// </summary>
        ///
        /// <param name="uow">              The uow. </param>
        /// <param name="languageIsoNames"> List of names of the language isoes. </param>
        ///
        /// <returns>
        /// A List&lt;LanguageEntity&gt;
        /// </returns>
        protected List<LanguageEntity> EnsureLanguages(ILocalizationUnitOfWork uow, IEnumerable<string> languageIsoNames)
        {
            var existing = uow.LanguageRepository.GetAll().ToList();

            foreach (var iso in languageIsoNames)
            {
                if (existing.All(l => l.IsoName != iso))
                {
                    // language doesnt exist yet - create it
                    existing.Add(uow.LanguageRepository.Add(new LanguageEntity {IsoName = iso, Name = iso}));
                }
            }

            return existing;
        }

        /// <summary>
        /// Ensures that authors.
        /// </summary>
        ///
        /// <param name="uow">          The uow. </param>
        /// <param name="authorNames">  List of names of the authors. </param>
        ///
        /// <returns>
        /// A List&lt;AuthorEntity&gt;
        /// </returns>
        protected List<AuthorEntity> EnsureAuthors(ILocalizationUnitOfWork uow, IEnumerable<string> authorNames)
        {
            var existing = uow.AuthorRepository.GetAll().ToList();

            foreach (var author in authorNames)
            {
                if (existing.All(a => a.Name != author))
                {
                    // auhtor doesnt exist yet - create it
                    existing.Add(uow.AuthorRepository.Add(new AuthorEntity {Name = author}));
                }
            }

            return existing;
        }

        /// <summary>
        /// Import asynchronous.
        /// </summary>
        ///
        /// <returns>
        /// The import.
        /// </returns>
        public Task<int> ImportAsync()
        {
            foreach (var source in LocalizationSources)
            {
                var resources = source.FindResources();
            }

            return Task.FromResult(0);
        }
    }
}