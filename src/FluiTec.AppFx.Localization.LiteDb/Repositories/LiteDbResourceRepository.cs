using System;
using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb.Repositories
{
    /// <summary>   A lite database resource repository. </summary>
    public class LiteDbResourceRepository : LiteDbWritableIntegerKeyTableDataRepository<ResourceEntity>,
        IResourceRepository
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbResourceRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        #endregion

        #region Repository

        /// <summary>   Adds entity. </summary>
        /// <param name="entity">   The entity to add. </param>
        /// <returns>   A TEntity. </returns>
        public override ResourceEntity Add(ResourceEntity entity)
        {
            return GetByKey(entity?.ResourceKey) ?? base.Add(entity);
        }

        /// <summary>   Adds a range of entities. </summary>
        /// <param name="entities"> An IEnumerable&lt;TEntity&gt; of items to append to this collection. </param>
        public override void AddRange(IEnumerable<ResourceEntity> entities)
        {
            foreach (var entity in entities)
                if (GetByKey(entity?.ResourceKey) == null)
                    Collection.Insert(entity);
        }

        /// <summary>   Updates the given entity. </summary>
        /// <exception cref="InvalidOperationException">
        ///     Thrown when the requested operation is
        ///     invalid.
        /// </exception>
        /// <param name="entity">   The entity to add. </param>
        /// <returns>   A TEntity. </returns>
        public override ResourceEntity Update(ResourceEntity entity)
        {
            var original = Get(entity.Id);

            // if key wasnt changed - continue as usual
            if (original.ResourceKey == entity.ResourceKey)
                return base.Update(entity);

            // if key was changed - make sure a corresponding one doesnt exist
            if (GetByKey(entity.ResourceKey) == null)
                return base.Update(entity);

            throw new InvalidOperationException("Duplicate key cannot be created");
        }

        /// <summary>   Gets by key. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>   The by key. </returns>
        public ResourceEntity GetByKey(string key)
        {
            return Collection.Find(e => e.ResourceKey == key).SingleOrDefault();
        }

        /// <summary>   Resets the synchronise status. </summary>
        public void ResetSyncStatus()
        {
            foreach (var entity in Collection.FindAll())
                entity.FromCode = false;
        }

        /// <summary>   Refactor key. </summary>
        /// <param name="oldKey">   The old key. </param>
        /// <param name="newKey">   The new key. </param>
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public bool RefactorKey(string oldKey, string newKey)
        {
            var entity = GetByKey(oldKey);
            if (entity == null) return false;

            entity.ResourceKey = newKey;
            entity.FromCode = true;
            Update(entity);
            return true;
        }

        /// <summary>   Gets the key begins withs in this collection. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the key begins withs in this
        ///     collection.
        /// </returns>
        public IEnumerable<ResourceEntity> GetByKeyBeginsWith(string key)
        {
            return Collection.Find(r => r.ResourceKey.StartsWith(key));
        }

        #endregion
    }
}