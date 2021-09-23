using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Dapper.Pgsql.Repositories;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Pgsql
{
    /// <summary>
    /// A mssql localization unit of work.
    /// </summary>
    public class PgsqlLocalizationUnitOfWork : DapperLocalizationUnitOfWork
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public PgsqlLocalizationUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public PgsqlLocalizationUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
        }

        /// <summary>
        /// Creates resource repository.
        /// </summary>
        ///
        /// <param name="uow">  The uow. </param>
        /// <param name="log">  The log. </param>
        ///
        /// <returns>
        /// The new resource repository.
        /// </returns>
        protected override IResourceRepository CreateResourceRepository(DapperLocalizationUnitOfWork uow,
            ILogger<IRepository> log)
            => new PgsqlResourceRepository(uow, log);
    }
}