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
    ///     A dapper resource repository.
    /// </summary>
    public abstract class DapperResourceRepository : DapperWritableKeyTableDataRepository<ResourceEntity, int>,
        IResourceRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperResourceRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>
        ///     Gets a resource entity using the given key.
        /// </summary>
        /// <param name="key">  The key to get. </param>
        /// <returns>
        ///     A ResourceEntity.
        /// </returns>
        public ResourceEntity Get(string key)
        {
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(ResourceEntity), nameof(ResourceEntity.Key)),
                nameof(Get), nameof(key));

            return UnitOfWork.Connection.QuerySingle<ResourceEntity>(command, new {Key = key}, UnitOfWork.Transaction);
        }

        /// <summary>
        ///     Gets an asynchronous.
        /// </summary>
        /// <param name="key">  The key to get. </param>
        /// <returns>
        ///     The asynchronous.
        /// </returns>
        public Task<ResourceEntity> GetAsync(string key)
        {
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(ResourceEntity), nameof(ResourceEntity.Key)),
                nameof(Get), nameof(key));

            return UnitOfWork.Connection.QuerySingleAsync<ResourceEntity>(command, new {Key = key},
                UnitOfWork.Transaction);
        }

        /// <summary>
        ///     Gets the key prefixes in this collection.
        /// </summary>
        /// <param name="keyPrefix">    The key prefix. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the key prefixes in this collection.
        /// </returns>
        public abstract IEnumerable<ResourceEntity> GetByKeyPrefix(string keyPrefix);

        /// <summary>
        ///     Gets by key prefix asynchronous.
        /// </summary>
        /// <param name="keyPrefix">    The key prefix. </param>
        /// <returns>
        ///     The by key prefix.
        /// </returns>
        public abstract Task<IEnumerable<ResourceEntity>> GetByKeyPrefixAsync(string keyPrefix);

        /// <summary>
        ///     Gets the authors in this collection.
        /// </summary>
        /// <param name="author">   The author. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the authors in this collection.
        /// </returns>
        public IEnumerable<ResourceEntity> GetByAuthor(AuthorEntity author)
        {
            return GetByAuthor(author.Id);
        }

        /// <summary>
        ///     Gets by author asynchronous.
        /// </summary>
        /// <param name="author">   The author. </param>
        /// <returns>
        ///     The by author.
        /// </returns>
        public Task<IEnumerable<ResourceEntity>> GetByAuthorAsync(AuthorEntity author)
        {
            return GetByAuthorAsync(author.Id);
        }

        /// <summary>
        ///     Gets the authors in this collection.
        /// </summary>
        /// <param name="authorId"> Identifier for the author. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the authors in this collection.
        /// </returns>
        public IEnumerable<ResourceEntity> GetByAuthor(int authorId)
        {
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(ResourceEntity), nameof(ResourceEntity.AuthorId)),
                nameof(GetByAuthor), nameof(authorId));

            return UnitOfWork.Connection.Query<ResourceEntity>(command, new {AuthorId = authorId},
                UnitOfWork.Transaction);
        }

        /// <summary>
        ///     Gets by author asynchronous.
        /// </summary>
        /// <param name="authorId"> Identifier for the author. </param>
        /// <returns>
        ///     The by author.
        /// </returns>
        public Task<IEnumerable<ResourceEntity>> GetByAuthorAsync(int authorId)
        {
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(ResourceEntity), nameof(ResourceEntity.AuthorId)),
                nameof(GetByAuthor), nameof(authorId));
            return UnitOfWork.Connection.QueryAsync<ResourceEntity>(command, new {AuthorId = authorId},
                UnitOfWork.Transaction);
        }
    }
}