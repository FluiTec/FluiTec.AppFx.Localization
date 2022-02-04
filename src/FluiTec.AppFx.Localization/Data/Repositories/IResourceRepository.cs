using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>
    ///     Interface for resource repository.
    /// </summary>
    public interface IResourceRepository : IWritableKeyTableDataRepository<ResourceEntity, int>
    {
        /// <summary>
        ///     Gets a resource entity using the given key.
        /// </summary>
        /// <param name="key">  The key to get. </param>
        /// <returns>
        ///     A ResourceEntity.
        /// </returns>
        ResourceEntity Get(string key);

        /// <summary>
        ///     Gets an asynchronous.
        /// </summary>
        /// <param name="key">  The key to get. </param>
        /// <returns>
        ///     The asynchronous.
        /// </returns>
        Task<ResourceEntity> GetAsync(string key);

        /// <summary>
        ///     Gets the key prefixes in this collection.
        /// </summary>
        /// <param name="keyPrefix">    The key prefix. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the key prefixes in this collection.
        /// </returns>
        IEnumerable<ResourceEntity> GetByKeyPrefix(string keyPrefix);

        /// <summary>
        ///     Gets by key prefix asynchronous.
        /// </summary>
        /// <param name="keyPrefix">    The key prefix. </param>
        /// <returns>
        ///     The by key prefix.
        /// </returns>
        Task<IEnumerable<ResourceEntity>> GetByKeyPrefixAsync(string keyPrefix);

        /// <summary>
        ///     Gets the authors in this collection.
        /// </summary>
        /// <param name="author">   The author. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the authors in this collection.
        /// </returns>
        IEnumerable<ResourceEntity> GetByAuthor(AuthorEntity author);

        /// <summary>
        ///     Gets by author asynchronous.
        /// </summary>
        /// <param name="author">   The author. </param>
        /// <returns>
        ///     The by author.
        /// </returns>
        Task<IEnumerable<ResourceEntity>> GetByAuthorAsync(AuthorEntity author);

        /// <summary>
        ///     Gets the authors in this collection.
        /// </summary>
        /// <param name="authorId"> Identifier for the author. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the authors in this collection.
        /// </returns>
        IEnumerable<ResourceEntity> GetByAuthor(int authorId);

        /// <summary>
        ///     Gets by author asynchronous.
        /// </summary>
        /// <param name="authorId"> Identifier for the author. </param>
        /// <returns>
        ///     The by author.
        /// </returns>
        Task<IEnumerable<ResourceEntity>> GetByAuthorAsync(int authorId);
    }
}