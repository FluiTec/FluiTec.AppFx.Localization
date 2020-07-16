using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Dapper.Repositories;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.Repositories
{
    /// <summary>   A mssql resource repository. </summary>
    public class MssqlResourceRepository : ResourceRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public MssqlResourceRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>   Resets the synchronise status. </summary>
        public override void ResetSyncStatus()
        {
            var command = $"UPDATE {TableName} SET {nameof(ResourceEntity.FromCode)} = @FromCode";
            UnitOfWork.Connection.Execute(command, new {FromCode = false}, UnitOfWork.Transaction);
        }

        /// <summary>   Refactor key. </summary>
        /// <param name="oldKey">   The old key. </param>
        /// <param name="newKey">   The new key. </param>
        /// <returns>   True if it succeeds, false if it fails. </returns>
        public override bool RefactorKey(string oldKey, string newKey)
        {
            var command =
                $"UPDATE {TableName} SET {nameof(ResourceEntity.ResourceKey)} @NewKey WHERE {nameof(ResourceEntity.ResourceKey)} = @OldKey";
            return UnitOfWork.Connection.Execute(command, new {NewKey = newKey, OldKey = oldKey},
                UnitOfWork.Transaction) > 0;
        }

        /// <summary>   Gets the key begins withs in this collection. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the key begins withs in this
        ///     collection.
        /// </returns>
        public override IEnumerable<ResourceEntity> GetByKeyBeginsWith(string key)
        {
            var command = $"SELECT * FROM {TableName} WHERE {nameof(ResourceEntity.ResourceKey)} LIKE @key + '%'";
            return UnitOfWork.Connection.Query<ResourceEntity>(command, new {key}, UnitOfWork.Transaction);
        }
    }
}