using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Dapper.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Sqlite.Repositories
{
    /// <summary>   A Sqlite translation repository. </summary>
    public class SqliteTranslationRepository : TranslationRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public SqliteTranslationRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>   Creates get all compound query command. </summary>
        /// <returns>   The new get all compound query command. </returns>
        protected override string CreateGetAllCompoundQueryCommand()
        {
            return $"SELECT * FROM {UnitOfWork.GetRepository<IResourceRepository>().TableName} AS resource" +
                   $" LEFT JOIN {TableName} AS translation" +
                   $" ON resource.{nameof(ResourceEntity.Id)} = translation.{nameof(TranslationEntity.ResourceId)}";
        }
    }
}
