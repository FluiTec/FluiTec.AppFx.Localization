using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb.Repositories
{
    /// <summary>
    ///     A lite database translation repository.
    /// </summary>
    public class LiteDbTranslationRepository : LiteDbWritableIntegerKeyTableDataRepository<TranslationEntity>,
        ITranslationRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbTranslationRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>
        ///     Gets the resources in this collection.
        /// </summary>
        /// <param name="resource"> The resource. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByResource(ResourceEntity resource)
        {
            return GetByResource(resource.Id);
        }

        /// <summary>
        /// Gets the resource compounds in this collection.
        /// </summary>
        ///
        /// <param name="resource"> The resource. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resource compounds in this
        /// collection.
        /// </returns>
        public IEnumerable<CompoundTranslationEntity> GetByResourceCompound(ResourceEntity resource)
        {
            return GetByResourceCompound(resource.Id);
        }

        /// <summary>
        ///     Gets by resource asynchronous.
        /// </summary>
        /// <param name="resource"> The resource. </param>
        /// <returns>
        ///     The by resource.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByResourceAsync(ResourceEntity resource)
        {
            return GetByResourceAsync(resource.Id);
        }

        /// <summary>
        /// Gets by resource compound asynchronous.
        /// </summary>
        ///
        /// <param name="resource"> The resource. </param>
        ///
        /// <returns>
        /// The by resource compound.
        /// </returns>
        public Task<IEnumerable<CompoundTranslationEntity>> GetByResourceCompoundAsync(ResourceEntity resource)
        {
            return Task.FromResult(GetByResourceCompound(resource));
        }

        /// <summary>
        ///     Gets the resources in this collection.
        /// </summary>
        /// <param name="resourceId">   Identifier for the resource. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByResource(int resourceId)
        {
            return Collection.Find(entity => entity.ResourceId == resourceId);
        }

        /// <summary>
        /// Gets the resource compounds in this collection.
        /// </summary>
        ///
        /// <param name="resourceId">   Identifier for the resource. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resource compounds in this
        /// collection.
        /// </returns>
        public IEnumerable<CompoundTranslationEntity> GetByResourceCompound(int resourceId)
        {
            var authors = UnitOfWork.GetRepository<IAuthorRepository>().GetAll();
            var languages = UnitOfWork.GetRepository<ILanguageRepository>().GetAll();
            var resource = UnitOfWork.GetRepository<IResourceRepository>().Get(resourceId);
            var translations = GetAll();

            return translations
                .Where(t => t.ResourceId == resource.Id)
                .Select(t => new CompoundTranslationEntity
                {
                    Translation = t,
                    Resource = resource,
                    Language = languages.Single(l => l.Id == t.LanguageId),
                    Author = authors.Single(a => a.Id == resource.AuthorId)
                });
        }

        /// <summary>
        ///     Gets by resource asynchronous.
        /// </summary>
        /// <param name="resourceId">   Identifier for the resource. </param>
        /// <returns>
        ///     The by resource.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByResourceAsync(int resourceId)
        {
            return Task.FromResult(GetByResource(resourceId));
        }

        /// <summary>
        /// Gets by resource compound asynchronous.
        /// </summary>
        ///
        /// <param name="resourceId">   Identifier for the resource. </param>
        ///
        /// <returns>
        /// The by resource compound.
        /// </returns>
        public Task<IEnumerable<CompoundTranslationEntity>> GetByResourceCompoundAsync(int resourceId)
        {
            return Task.FromResult(GetByResourceCompound(resourceId));
        }

        /// <summary>
        ///     Gets the resources in this collection.
        /// </summary>
        /// <param name="resourceKey">  The resource key. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByResource(string resourceKey)
        {
            var resource = UnitOfWork.GetRepository<IResourceRepository>().Get(resourceKey);
            return Collection.Find(entity => entity.ResourceId == resource.Id);
        }

        /// <summary>
        /// Gets the resource compounds in this collection.
        /// </summary>
        ///
        /// <param name="resourceKey">  The resource key. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resource compounds in this
        /// collection.
        /// </returns>
        public IEnumerable<CompoundTranslationEntity> GetByResourceCompound(string resourceKey)
        {
            var authors = UnitOfWork.GetRepository<IAuthorRepository>().GetAll();
            var languages = UnitOfWork.GetRepository<ILanguageRepository>().GetAll();
            var resource = UnitOfWork.GetRepository<IResourceRepository>().Get(resourceKey);
            var translations = GetAll();

            return translations
                .Where(t => t.ResourceId == resource.Id)
                .Select(t => new CompoundTranslationEntity
                {
                    Translation = t,
                    Resource = resource,
                    Language = languages.Single(l => l.Id == t.LanguageId),
                    Author = authors.Single(a => a.Id == resource.AuthorId)
                });
        }

        /// <summary>
        ///     Gets by resource asynchronous.
        /// </summary>
        /// <param name="resourceKey">  The resource key. </param>
        /// <returns>
        ///     The by resource.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByResourceAsync(string resourceKey)
        {
            return Task.FromResult(GetByResource(resourceKey));
        }

        /// <summary>
        /// Gets by resource compound asynchronous.
        /// </summary>
        ///
        /// <param name="resourceKey">  The resource key. </param>
        ///
        /// <returns>
        /// The by resource compound.
        /// </returns>
        public Task<IEnumerable<CompoundTranslationEntity>> GetByResourceCompoundAsync(string resourceKey)
        {
            return Task.FromResult(GetByResourceCompound(resourceKey));
        }

        /// <summary>
        ///     Gets the languages in this collection.
        /// </summary>
        /// <param name="language"> The language. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByLanguage(LanguageEntity language)
        {
            return GetByLanguage(language.Id);
        }

        /// <summary>
        ///     Gets by language asynchronous.
        /// </summary>
        /// <param name="language"> The language. </param>
        /// <returns>
        ///     The by language.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(LanguageEntity language)
        {
            return GetByLanguageAsync(language.Id);
        }

        /// <summary>
        ///     Gets the languages in this collection.
        /// </summary>
        /// <param name="languageId">   Identifier for the language. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByLanguage(int languageId)
        {
            return Collection.Find(entity => entity.LanguageId == languageId);
        }

        /// <summary>
        ///     Gets by language asynchronous.
        /// </summary>
        /// <param name="languageId">   Identifier for the language. </param>
        /// <returns>
        ///     The by language.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(int languageId)
        {
            return Task.FromResult(GetByLanguage(languageId));
        }

        /// <summary>
        ///     Gets the languages in this collection.
        /// </summary>
        /// <param name="isoName">  Name of the ISO. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByLanguage(string isoName)
        {
            var language = UnitOfWork.GetRepository<ILanguageRepository>().Get(isoName);
            return Collection.Find(entity => entity.LanguageId == language.Id);
        }

        /// <summary>
        ///     Gets by language asynchronous.
        /// </summary>
        /// <param name="isoName">  Name of the ISO. </param>
        /// <returns>
        ///     The by language.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(string isoName)
        {
            return Task.FromResult(GetByLanguage(isoName));
        }

        /// <summary>
        /// Gets the languages in this collection.
        /// </summary>
        ///
        /// <param name="languages">    The languages. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public IEnumerable<CompoundTranslationEntity> GetByLanguages(IEnumerable<LanguageEntity> languages)
        {
            return GetByLanguages(languages.Select(l => l.Id));
        }

        /// <summary>
        /// Gets by languages asynchronous.
        /// </summary>
        ///
        /// <param name="languages">    The languages. </param>
        ///
        /// <returns>
        /// The by languages.
        /// </returns>
        public Task<IEnumerable<CompoundTranslationEntity>> GetByLanguagesAsync(IEnumerable<LanguageEntity> languages)
        {
            return GetByLanguagesAsync(languages.Select(l => l.Id));
        }

        /// <summary>
        /// Gets the languages in this collection.
        /// </summary>
        ///
        /// <param name="languageIds">  List of identifiers for the languages. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public IEnumerable<CompoundTranslationEntity> GetByLanguages(IEnumerable<int> languageIds)
        {
            var authors = UnitOfWork.GetRepository<IAuthorRepository>().GetAll();
            var languages = UnitOfWork.GetRepository<ILanguageRepository>().GetAll();
            var resources = UnitOfWork.GetRepository<IResourceRepository>().GetAll().ToList();
            var translations = GetAll();

            return translations
                .Where(t => languageIds.Contains(t.LanguageId))
                .Select(t => new CompoundTranslationEntity
                {
                    Translation = t,
                    Resource = resources.Single(r => r.Id == t.ResourceId),
                    Language = languages.Single(l => l.Id == t.LanguageId),
                    Author = authors.Single(a => a.Id == resources.Single(r => r.Id == t.ResourceId).AuthorId)
                });
        }

        /// <summary>
        /// Gets by languages asynchronous.
        /// </summary>
        ///
        /// <param name="languageIds">  List of identifiers for the languages. </param>
        ///
        /// <returns>
        /// The by languages.
        /// </returns>
        public Task<IEnumerable<CompoundTranslationEntity>> GetByLanguagesAsync(IEnumerable<int> languageIds)
        {
            return Task.FromResult(GetByLanguages(languageIds));
        }
    }
}