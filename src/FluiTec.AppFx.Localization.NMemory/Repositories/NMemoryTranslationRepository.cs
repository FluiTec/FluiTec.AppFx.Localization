using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.NMemory.Repositories
{
    /// <summary>
    ///     A memory translation repository.
    /// </summary>
    public class NMemoryTranslationRepository : NMemoryWritableKeyTableDataRepository<TranslationEntity, int>,
        ITranslationRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryTranslationRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(
            unitOfWork, logger)
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
            return Table.Where(e => e.ResourceId == resource.Id);
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
            return Task.FromResult(GetByResource(resource));
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
            return GetByResourceCompoundAsync(resource.Id);
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
            return Table.Where(e => e.ResourceId == resourceId);
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
            var resourceTable = UnitOfWork.NMemoryDataService.GetTable<ResourceEntity>();
            var languageTable = UnitOfWork.NMemoryDataService.GetTable<LanguageEntity>();
            var authorTable = UnitOfWork.NMemoryDataService.GetTable<AuthorEntity>();

            return Table
                .Join(resourceTable, 
                    translation => translation.ResourceId, 
                    resource => resource.Id,
                    (translation, resource) => new CompoundTranslationEntity
                    {
                        Translation = translation, 
                        Resource = resource
                    })
                .Join(languageTable, 
                    translation => translation.Translation.LanguageId, 
                    language => language.Id, 
                    (entity, language) => new CompoundTranslationEntity
                    {
                        Translation = entity.Translation, 
                        Resource = entity.Resource,
                        Language = language
                    })
                .Join(authorTable, 
                    translation => translation.Resource.AuthorId, 
                    author => author.Id, 
                    (entity, author) => new CompoundTranslationEntity
                    {
                        Translation = entity.Translation, 
                        Resource = entity.Resource,
                        Language = entity.Language,
                        Author = author
                    })
                .Where(t => t.Resource.Id == resourceId);
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
            return GetByResource(resource);
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
            var resourceTable = UnitOfWork.NMemoryDataService.GetTable<ResourceEntity>();
            var languageTable = UnitOfWork.NMemoryDataService.GetTable<LanguageEntity>();
            var authorTable = UnitOfWork.NMemoryDataService.GetTable<AuthorEntity>();

            return Table
                .Join(resourceTable, 
                    translation => translation.ResourceId, 
                    resource => resource.Id,
                    (translation, resource) => new CompoundTranslationEntity
                    {
                        Translation = translation, 
                        Resource = resource
                    })
                .Join(languageTable, 
                    translation => translation.Translation.LanguageId, 
                    language => language.Id, 
                    (entity, language) => new CompoundTranslationEntity
                    {
                        Translation = entity.Translation, 
                        Resource = entity.Resource,
                        Language = language
                    })
                .Join(authorTable, 
                    translation => translation.Resource.AuthorId, 
                    author => author.Id, 
                    (entity, author) => new CompoundTranslationEntity
                    {
                        Translation = entity.Translation, 
                        Resource = entity.Resource,
                        Language = entity.Language,
                        Author = author
                    })
                .Where(t => t.Resource.ResourceKey == resourceKey);
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
        /// <param name="resourceKey">  The resource ke. </param>
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
            return Table.Where(e => e.LanguageId == language.Id);
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
            return Task.FromResult(GetByLanguage(language));
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
            return Table.Where(e => e.LanguageId == languageId);
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
            return GetByLanguage(language);
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
            var resourceTable = UnitOfWork.NMemoryDataService.GetTable<ResourceEntity>();
            var languageTable = UnitOfWork.NMemoryDataService.GetTable<LanguageEntity>();
            var authorTable = UnitOfWork.NMemoryDataService.GetTable<AuthorEntity>();

            return Table
                .Join(resourceTable, 
                translation => translation.ResourceId, 
                resource => resource.Id,
                (translation, resource) => new CompoundTranslationEntity
                    {
                        Translation = translation, 
                        Resource = resource
                    })
                .Join(languageTable, 
                    translation => translation.Translation.LanguageId, 
                    language => language.Id, 
                    (entity, language) => new CompoundTranslationEntity
                    {
                        Translation = entity.Translation, 
                        Resource = entity.Resource,
                        Language = language
                    })
                .Join(authorTable, 
                    translation => translation.Resource.AuthorId, 
                    author => author.Id, 
                    (entity, author) => new CompoundTranslationEntity
                    {
                        Translation = entity.Translation, 
                        Resource = entity.Resource,
                        Language = entity.Language,
                        Author = author
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

        /// <summary>
        /// Gets the resource suffix compounds in this collection.
        /// </summary>
        ///
        /// <param name="suffix">   The suffix. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resource suffix compounds in this
        /// collection.
        /// </returns>
        public IEnumerable<CompoundTranslationEntity> GetByResourceSuffixCompound(string suffix)
        {
            var resourceTable = UnitOfWork.NMemoryDataService.GetTable<ResourceEntity>();
            var languageTable = UnitOfWork.NMemoryDataService.GetTable<LanguageEntity>();
            var authorTable = UnitOfWork.NMemoryDataService.GetTable<AuthorEntity>();

            return Table
                .Join(resourceTable,
                    translation => translation.ResourceId,
                    resource => resource.Id,
                    (translation, resource) => new CompoundTranslationEntity
                    {
                        Translation = translation,
                        Resource = resource
                    })
                .Join(languageTable,
                    translation => translation.Translation.LanguageId,
                    language => language.Id,
                    (entity, language) => new CompoundTranslationEntity
                    {
                        Translation = entity.Translation,
                        Resource = entity.Resource,
                        Language = language
                    })
                .Join(authorTable,
                    translation => translation.Resource.AuthorId,
                    author => author.Id,
                    (entity, author) => new CompoundTranslationEntity
                    {
                        Translation = entity.Translation,
                        Resource = entity.Resource,
                        Language = entity.Language,
                        Author = author
                    })
                .Where(t => t.Resource.ResourceKey.StartsWith(suffix));
        }

        /// <summary>
        /// Gets by resource suffix compound asynchronous.
        /// </summary>
        ///
        /// <param name="suffix">   The suffix. </param>
        ///
        /// <returns>
        /// The by resource suffix compound.
        /// </returns>
        public Task<IEnumerable<CompoundTranslationEntity>> GetByResourceSuffixCompoundAsync(string suffix)
        {
            return Task.FromResult(GetByResourceSuffixCompound(suffix));
        }
    }
}