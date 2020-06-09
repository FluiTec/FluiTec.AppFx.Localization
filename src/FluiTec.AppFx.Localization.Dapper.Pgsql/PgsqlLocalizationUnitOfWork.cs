using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Dapper.Pgsql.Repositories;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Pgsql
{
    /// <summary>   A Pgsql localization unit of work. </summary>
    public class PgsqlLocalizationUnitOfWork : DapperLocalizationUnitOfWork
    {
        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public PgsqlLocalizationUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
        }

        /// <summary>   Constructor. </summary>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public PgsqlLocalizationUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
        }

        /// <summary>   Registers the repositories. </summary>
        protected override void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IResourceRepository), (uow, log) => new PgsqlResourceRepository((DapperLocalizationUnitOfWork)uow, log));
            RepositoryProviders.Add(typeof(ITranslationRepository), (uow, log) => new PgsqlTranslationRepository((DapperLocalizationUnitOfWork)uow, log));
        }
    }
}