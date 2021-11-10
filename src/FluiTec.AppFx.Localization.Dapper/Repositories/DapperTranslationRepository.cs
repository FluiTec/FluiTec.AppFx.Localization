using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Repositories
{
    /// <summary>
    ///     A dapper translation repository.
    /// </summary>
    public abstract class DapperTranslationRepository : DapperWritableKeyTableDataRepository<TranslationEntity, int>,
        ITranslationRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperTranslationRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
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
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(TranslationEntity), nameof(TranslationEntity.ResourceId)),
                nameof(GetByResource), nameof(resourceId));

            return UnitOfWork.Connection.Query<TranslationEntity>(command, new {ResourceId = resourceId},
                UnitOfWork.Transaction);
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
        public abstract IEnumerable<CompoundTranslationEntity> GetByResourceCompound(int resourceId);

        /// <summary>
        ///     Gets by resource asynchronous.
        /// </summary>
        /// <param name="resourceId">   Identifier for the resource. </param>
        /// <returns>
        ///     The by resource.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByResourceAsync(int resourceId)
        {
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(TranslationEntity), nameof(TranslationEntity.ResourceId)),
                nameof(GetByResource), nameof(resourceId));

            return UnitOfWork.Connection.QueryAsync<TranslationEntity>(command, new {ResourceId = resourceId},
                UnitOfWork.Transaction);
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
        public abstract Task<IEnumerable<CompoundTranslationEntity>> GetByResourceCompoundAsync(int resourceId);

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
            return GetByResource(resource.Id);
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
        public abstract IEnumerable<CompoundTranslationEntity> GetByResourceCompound(string resourceKey);

        /// <summary>
        ///     Gets by resource asynchronous.
        /// </summary>
        /// <param name="resourceKey">  The resource key. </param>
        /// <returns>
        ///     The by resource.
        /// </returns>
        public async Task<IEnumerable<TranslationEntity>> GetByResourceAsync(string resourceKey)
        {
            var resource = await UnitOfWork.GetRepository<IResourceRepository>().GetAsync(resourceKey);
            return await GetByResourceAsync(resource.Id);
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
        public abstract Task<IEnumerable<CompoundTranslationEntity>> GetByResourceCompoundAsync(string resourceKey);

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
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(TranslationEntity), nameof(TranslationEntity.LanguageId)),
                nameof(GetByLanguage), nameof(languageId));

            return UnitOfWork.Connection.Query<TranslationEntity>(command, new {LanguageId = languageId},
                UnitOfWork.Transaction);
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
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(TranslationEntity), nameof(TranslationEntity.LanguageId)),
                nameof(GetByLanguage), nameof(languageId));

            return UnitOfWork.Connection.QueryAsync<TranslationEntity>(command, new {LanguageId = languageId},
                UnitOfWork.Transaction);
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
            return GetByLanguage(language.Id);
        }

        /// <summary>
        ///     Gets by language asynchronous.
        /// </summary>
        /// <param name="isoName">  Name of the ISO. </param>
        /// <returns>
        ///     The by language.
        /// </returns>
        public async Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(string isoName)
        {
            var language = await UnitOfWork.GetRepository<ILanguageRepository>().GetAsync(isoName);
            return await GetByLanguageAsync(language.Id);
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
        public abstract IEnumerable<CompoundTranslationEntity> GetByLanguages(IEnumerable<int> languageIds);

        /// <summary>
        /// Gets by languages asynchronous.
        /// </summary>
        ///
        /// <param name="languageIds">  List of identifiers for the languages. </param>
        ///
        /// <returns>
        /// The by languages.
        /// </returns>
        public abstract Task<IEnumerable<CompoundTranslationEntity>> GetByLanguagesAsync(IEnumerable<int> languageIds);

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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}