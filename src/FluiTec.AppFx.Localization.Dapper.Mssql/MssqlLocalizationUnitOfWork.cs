﻿using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Dapper.Mssql.Repositories;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Mssql
{
    /// <summary>   A mssql localization unit of work. </summary>
    public class MssqlLocalizationUnitOfWork : DapperLocalizationUnitOfWork
    {
        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public MssqlLocalizationUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(
            dataService, logger)
        {
        }

        /// <summary>   Constructor. </summary>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public MssqlLocalizationUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService,
            ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
        }

        /// <summary>   Registers the repositories. </summary>
        protected override void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IResourceRepository),
                (uow, log) => new MssqlResourceRepository((DapperLocalizationUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(ITranslationRepository),
                (uow, log) => new MssqlTranslationRepository((DapperLocalizationUnitOfWork) uow, log));
        }
    }
}