using System.Collections.Generic;
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
    /// A dapper translation repository.
    /// </summary>
    public class DapperTranslationRepository : DapperWritableKeyTableDataRepository<TranslationEntity, int>, ITranslationRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperTranslationRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Gets the resources in this collection.
        /// </summary>
        ///
        /// <param name="resource"> The resource. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByResource(ResourceEntity resource) => GetByResource(resource.Id);

        /// <summary>
        /// Gets by resource asynchronous.
        /// </summary>
        ///
        /// <param name="resource"> The resource. </param>
        ///
        /// <returns>
        /// The by resource.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByResourceAsync(ResourceEntity resource) => GetByResourceAsync(resource.Id);

        /// <summary>
        /// Gets the resources in this collection.
        /// </summary>
        ///
        /// <param name="resourceId">   Identifier for the resource. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByResource(int resourceId)
        {
            var command = SqlBuilder.SelectByFilter(typeof(TranslationEntity), nameof(TranslationEntity.ResourceId));
            return UnitOfWork.Connection.Query<TranslationEntity>(command, new {ResourceId = resourceId}, UnitOfWork.Transaction);
        }

        /// <summary>
        /// Gets by resource asynchronous.
        /// </summary>
        ///
        /// <param name="resourceId">   Identifier for the resource. </param>
        ///
        /// <returns>
        /// The by resource.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByResourceAsync(int resourceId)
        {
            var command = SqlBuilder.SelectByFilter(typeof(TranslationEntity), nameof(TranslationEntity.ResourceId));
            return UnitOfWork.Connection.QueryAsync<TranslationEntity>(command, new {ResourceId = resourceId}, UnitOfWork.Transaction);
        }

        /// <summary>
        /// Gets the resources in this collection.
        /// </summary>
        ///
        /// <param name="resourceKey">  The resource key. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByResource(string resourceKey)
        {
            var resource = UnitOfWork.GetRepository<IResourceRepository>().Get(resourceKey);
            return GetByResource(resource.Id);
        }

        /// <summary>
        /// Gets by resource asynchronous.
        /// </summary>
        ///
        /// <param name="resourceKey">  The resource key. </param>
        ///
        /// <returns>
        /// The by resource.
        /// </returns>
        public async Task<IEnumerable<TranslationEntity>> GetByResourceAsync(string resourceKey)
        {
            var resource = await UnitOfWork.GetRepository<IResourceRepository>().GetAsync(resourceKey);
            return await GetByResourceAsync(resource.Id);
        }

        /// <summary>
        /// Gets the languages in this collection.
        /// </summary>
        ///
        /// <param name="language"> The language. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByLanguage(LanguageEntity language) => GetByLanguage(language.Id);

        /// <summary>
        /// Gets by language asynchronous.
        /// </summary>
        ///
        /// <param name="language"> The language. </param>
        ///
        /// <returns>
        /// The by language.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(LanguageEntity language) => GetByLanguageAsync(language.Id);

        /// <summary>
        /// Gets the languages in this collection.
        /// </summary>
        ///
        /// <param name="languageId">   Identifier for the language. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByLanguage(int languageId)
        {
            var command = SqlBuilder.SelectByFilter(typeof(TranslationEntity), nameof(TranslationEntity.LanguageId));
            return UnitOfWork.Connection.Query<TranslationEntity>(command, new {LanguageId = languageId}, UnitOfWork.Transaction);
        }

        /// <summary>
        /// Gets by language asynchronous.
        /// </summary>
        ///
        /// <param name="languageId">   Identifier for the language. </param>
        ///
        /// <returns>
        /// The by language.
        /// </returns>
        public Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(int languageId)
        {
            var command = SqlBuilder.SelectByFilter(typeof(TranslationEntity), nameof(TranslationEntity.LanguageId));
            return UnitOfWork.Connection.QueryAsync<TranslationEntity>(command, new {LanguageId = languageId}, UnitOfWork.Transaction);
        }

        /// <summary>
        /// Gets the languages in this collection.
        /// </summary>
        ///
        /// <param name="isoName">  Name of the ISO. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> GetByLanguage(string isoName)
        {
            var language = UnitOfWork.GetRepository<ILanguageRepository>().Get(isoName);
            return GetByLanguage(language.Id);
        }

        /// <summary>
        /// Gets by language asynchronous.
        /// </summary>
        ///
        /// <param name="isoName">  Name of the ISO. </param>
        ///
        /// <returns>
        /// The by language.
        /// </returns>
        public async Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(string isoName)
        {
            var language = await UnitOfWork.GetRepository<ILanguageRepository>().GetAsync(isoName);
            return await GetByLanguageAsync(language.Id);
        }
    }
}