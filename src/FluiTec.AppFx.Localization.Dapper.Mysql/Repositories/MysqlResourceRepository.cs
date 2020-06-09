using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Dapper.Repositories;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Mysql.Repositories
{
    /// <summary>   A mysql resource repository. </summary>
    public class MysqlResourceRepository : ResourceRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public MysqlResourceRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
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
    }
}