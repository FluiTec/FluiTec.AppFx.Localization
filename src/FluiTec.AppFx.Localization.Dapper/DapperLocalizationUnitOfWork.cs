using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper
{
    /// <summary>
    /// A dapper localization unit of work.
    /// </summary>
    public abstract class DapperLocalizationUnitOfWork : DapperUnitOfWork, ILocalizationUnitOfWork
    {
        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        protected DapperLocalizationUnitOfWork(IDapperDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterRepositories();
        }

        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        protected DapperLocalizationUnitOfWork(DapperUnitOfWork parentUnitOfWork, IDataService dataService, ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            RegisterRepositories();
        }

        /// <summary>
        /// Gets the author repository.
        /// </summary>
        ///
        /// <value>
        /// The author repository.
        /// </value>
        public IAuthorRepository AuthorRepository => GetRepository<IAuthorRepository>();

        /// <summary>
        /// Gets the language repository.
        /// </summary>
        ///
        /// <value>
        /// The language repository.
        /// </value>
        public ILanguageRepository LanguageRepository => GetRepository<ILanguageRepository>();

        /// <summary>
        /// Gets the resource repository.
        /// </summary>
        ///
        /// <value>
        /// The resource repository.
        /// </value>
        public IResourceRepository ResourceRepository => GetRepository<IResourceRepository>();

        /// <summary>
        /// Gets the translation repository.
        /// </summary>
        ///
        /// <value>
        /// The translation repository.
        /// </value>
        public ITranslationRepository TranslationRepository => GetRepository<ITranslationRepository>();

        /// <summary>
        /// Registers the repositories.
        /// </summary>
        protected abstract void RegisterRepositories();
    }
}