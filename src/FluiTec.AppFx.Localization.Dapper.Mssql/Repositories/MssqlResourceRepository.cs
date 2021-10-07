using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Dapper.Repositories;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.Repositories
{
    /// <summary>
    ///     A mssql resource repository.
    /// </summary>
    public class MssqlResourceRepository : DapperResourceRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public MssqlResourceRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>
        ///     Gets the key prefixes in this collection.
        /// </summary>
        /// <param name="keyPrefix">    The key prefix. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the key prefixes in this collection.
        /// </returns>
        public override IEnumerable<ResourceEntity> GetByKeyPrefix(string keyPrefix)
        {
            var command = GetFromCache(() =>
                    $"SELECT * FROM {SqlBuilder.Adapter.RenderTableName(typeof(ResourceEntity))} " +
                    $"WHERE {SqlBuilder.Adapter.RenderPropertyName(nameof(ResourceEntity.Key))} " +
                    $"LIKE {SqlBuilder.Adapter.RenderParameterProperty(nameof(keyPrefix))} + '%'",
                nameof(GetByKeyPrefix), nameof(keyPrefix));

            return UnitOfWork.Connection.Query<ResourceEntity>(command, new {keyPrefix}, UnitOfWork.Transaction);
        }

        /// <summary>
        ///     Gets by key prefix asynchronous.
        /// </summary>
        /// <param name="keyPrefix">    The key prefix. </param>
        /// <returns>
        ///     The by key prefix.
        /// </returns>
        public override Task<IEnumerable<ResourceEntity>> GetByKeyPrefixAsync(string keyPrefix)
        {
            var command = GetFromCache(() =>
                    $"SELECT * FROM {SqlBuilder.Adapter.RenderTableName(typeof(ResourceEntity))} " +
                    $"WHERE {SqlBuilder.Adapter.RenderPropertyName(nameof(ResourceEntity.Key))} " +
                    $"LIKE {SqlBuilder.Adapter.RenderParameterProperty(nameof(keyPrefix))} + '%'",
                nameof(GetByKeyPrefix), nameof(keyPrefix));

            return UnitOfWork.Connection.QueryAsync<ResourceEntity>(command, new {keyPrefix}, UnitOfWork.Transaction);
        }
    }
}