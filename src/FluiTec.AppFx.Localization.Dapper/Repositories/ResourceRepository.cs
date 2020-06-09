using Dapper;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Repositories
{
    /// <summary>A resource repository.</summary>
    public abstract class ResourceRepository : DapperWritableKeyTableDataRepository<ResourceEntity, int>, IResourceRepository
    {
        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected ResourceRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>   Gets by key. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>   The by key. </returns>
        public ResourceEntity GetByKey(string key)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(ResourceEntity.ResourceKey));
            return UnitOfWork.Connection.QuerySingleOrDefault<ResourceEntity>(command, new {ResourceKey = key},
                UnitOfWork.Transaction);
        }

        /// <summary>   Resets the synchronise status. </summary>
        public abstract void ResetSyncStatus();

        /// <summary>   Refactor key. </summary>
        /// <param name="oldKey">   The old key. </param>
        /// <param name="newKey">   The new key. </param>
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public abstract bool RefactorKey(string oldKey, string newKey);
    }
}