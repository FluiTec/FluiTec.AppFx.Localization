using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>
    /// Interface for resource repository.
    /// </summary>
    public interface IResourceRepository : IWritableKeyTableDataRepository<ResourceEntity, int>
    {
        /// <summary>
        /// Gets a resource entity using the given key.
        /// </summary>
        ///
        /// <param name="key">  The key to get. </param>
        ///
        /// <returns>
        /// A ResourceEntity.
        /// </returns>
        ResourceEntity Get(string key);

        /// <summary>
        /// Gets the key prefixes in this collection.
        /// </summary>
        ///
        /// <param name="keyPrefix">    The key prefix. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the key prefixes in this collection.
        /// </returns>
        IEnumerable<ResourceEntity> GetByKeyPrefix(string keyPrefix);

        /// <summary>
        /// Gets the authors in this collection.
        /// </summary>
        ///
        /// <param name="author">   The author. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the authors in this collection.
        /// </returns>
        IEnumerable<ResourceEntity> GetByAuthor(AuthorEntity author);

        /// <summary>
        /// Gets the authors in this collection.
        /// </summary>
        ///
        /// <param name="authorId"> Identifier for the author. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the authors in this collection.
        /// </returns>
        IEnumerable<ResourceEntity> GetByAuthor(int authorId);
    }
}