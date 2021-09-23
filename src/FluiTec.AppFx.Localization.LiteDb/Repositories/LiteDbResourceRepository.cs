using System.Collections.Generic;
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
    /// A lite database resource repository.
    /// </summary>
    public class LiteDbResourceRepository : LiteDbWritableIntegerKeyTableDataRepository<ResourceEntity>, IResourceRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbResourceRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Gets a resource entity using the given key.
        /// </summary>
        ///
        /// <param name="key">  The key to get. </param>
        ///
        /// <returns>
        /// A ResourceEntity.
        /// </returns>
        public ResourceEntity Get(string key)
        {
            return Collection.FindOne(entity => entity.Key == key);
        }

        /// <summary>
        /// Gets an asynchronous.
        /// </summary>
        ///
        /// <param name="key">  The key to get. </param>
        ///
        /// <returns>
        /// The asynchronous.
        /// </returns>
        public Task<ResourceEntity> GetAsync(string key)
        {
            return Task.FromResult(Get(key));
        }

        /// <summary>
        /// Gets the key prefixes in this collection.
        /// </summary>
        ///
        /// <param name="keyPrefix">    The key prefix. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the key prefixes in this collection.
        /// </returns>
        public IEnumerable<ResourceEntity> GetByKeyPrefix(string keyPrefix)
        {
            return Collection.Find(entity => entity.Key.StartsWith(keyPrefix));
        }

        /// <summary>
        /// Gets by key prefix asynchronous.
        /// </summary>
        ///
        /// <param name="keyPrefix">    The key prefix. </param>
        ///
        /// <returns>
        /// The by key prefix.
        /// </returns>
        public Task<IEnumerable<ResourceEntity>> GetByKeyPrefixAsync(string keyPrefix)
        {
            return Task.FromResult(GetByKeyPrefix(keyPrefix));
        }

        /// <summary>
        /// Gets the authors in this collection.
        /// </summary>
        ///
        /// <param name="author">   The author. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the authors in this collection.
        /// </returns>
        public IEnumerable<ResourceEntity> GetByAuthor(AuthorEntity author) => GetByAuthor(author.Id);

        /// <summary>
        /// Gets by author asynchronous.
        /// </summary>
        ///
        /// <param name="author">   The author. </param>
        ///
        /// <returns>
        /// The by author.
        /// </returns>
        public Task<IEnumerable<ResourceEntity>> GetByAuthorAsync(AuthorEntity author) => GetByAuthorAsync(author.Id);

        /// <summary>
        /// Gets the authors in this collection.
        /// </summary>
        ///
        /// <param name="authorId"> Identifier for the author. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the authors in this collection.
        /// </returns>
        public IEnumerable<ResourceEntity> GetByAuthor(int authorId)
        {
            return Collection.Find(entity => entity.AuthorId == authorId);
        }

        /// <summary>
        /// Gets by author asynchronous.
        /// </summary>
        ///
        /// <param name="authorId"> Identifier for the author. </param>
        ///
        /// <returns>
        /// The by author.
        /// </returns>
        public Task<IEnumerable<ResourceEntity>> GetByAuthorAsync(int authorId)
        {
            return Task.FromResult(GetByAuthor(authorId));
        }
    }
}