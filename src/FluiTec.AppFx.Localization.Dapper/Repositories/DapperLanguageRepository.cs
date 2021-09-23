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
    /// A dapper language repository.
    /// </summary>
    public class DapperLanguageRepository : DapperWritableKeyTableDataRepository<LanguageEntity, int>, ILanguageRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperLanguageRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Gets a language entity using the given ISO name.
        /// </summary>
        ///
        /// <param name="isoName">  The ISO name to get. </param>
        ///
        /// <returns>
        /// A LanguageEntity.
        /// </returns>
        public LanguageEntity Get(string isoName)
        {
            var command = SqlBuilder.SelectByFilter(typeof(LanguageEntity), nameof(LanguageEntity.IsoName));
            return UnitOfWork.Connection.QuerySingle<LanguageEntity>(command, new {IsoName = isoName}, UnitOfWork.Transaction);
        }

        /// <summary>
        /// Gets an asynchronous.
        /// </summary>
        ///
        /// <param name="isoName">  The ISO name to get. </param>
        ///
        /// <returns>
        /// The asynchronous.
        /// </returns>
        public Task<LanguageEntity> GetAsync(string isoName)
        {
            var command = SqlBuilder.SelectByFilter(typeof(LanguageEntity), nameof(LanguageEntity.IsoName));
            return UnitOfWork.Connection.QuerySingleAsync<LanguageEntity>(command, new {IsoName = isoName}, UnitOfWork.Transaction);
        }
    }
}