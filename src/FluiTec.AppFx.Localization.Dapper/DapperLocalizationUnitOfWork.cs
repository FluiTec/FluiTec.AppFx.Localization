using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper
{
    /// <summary>A dapper localization unit of work.</summary>
    public abstract class DapperLocalizationUnitOfWork : DapperUnitOfWork, ILocalizationUnitOfWork
    {
        protected DapperLocalizationUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(
            dataService, logger)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterRepositories();
        }

        protected DapperLocalizationUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService,
            ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterRepositories();
        }

        /// <summary>   Gets the resource repository. </summary>
        /// <value> The resource repository. </value>
        public IResourceRepository ResourceRepository => GetRepository<IResourceRepository>();

        /// <summary>   Gets the translation repository. </summary>
        /// <value> The translation repository. </value>
        public ITranslationRepository TranslationRepository => GetRepository<ITranslationRepository>();

        /// <summary>   Registers the repositories. </summary>
        protected abstract void RegisterRepositories();
    }
}