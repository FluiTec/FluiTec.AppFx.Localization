using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.DataServices;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.NMemory.Repositories;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.NMemory
{
    /// <summary>
    ///     A memory localization unit of work.
    /// </summary>
    public class NMemoryLocalizationUnitOfWork : NMemoryUnitOfWork, ILocalizationUnitOfWork
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryLocalizationUnitOfWork(INMemoryDataService dataService, ILogger<IUnitOfWork> logger) : base(
            dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="dataService">      The data service. </param>
        /// <param name="logger">           The logger. </param>
        public NMemoryLocalizationUnitOfWork(NMemoryUnitOfWork parentUnitOfWork, IDataService dataService,
            ILogger<IUnitOfWork> logger) : base(parentUnitOfWork, dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        ///     Gets the author repository.
        /// </summary>
        /// <value>
        ///     The author repository.
        /// </value>
        public IAuthorRepository AuthorRepository => GetRepository<IAuthorRepository>();

        /// <summary>
        ///     Gets the language repository.
        /// </summary>
        /// <value>
        ///     The language repository.
        /// </value>
        public ILanguageRepository LanguageRepository => GetRepository<ILanguageRepository>();

        /// <summary>
        ///     Gets the resource repository.
        /// </summary>
        /// <value>
        ///     The resource repository.
        /// </value>
        public IResourceRepository ResourceRepository => GetRepository<IResourceRepository>();

        /// <summary>
        ///     Gets the translation repository.
        /// </summary>
        /// <value>
        ///     The translation repository.
        /// </value>
        public ITranslationRepository TranslationRepository => GetRepository<ITranslationRepository>();

        /// <summary>
        ///     Registers the repositories.
        /// </summary>
        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IAuthorRepository), (uow, log)
                => new NMemoryAuthorRepository((NMemoryLocalizationUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(ILanguageRepository), (uow, log)
                => new NMemoryLanguageRepository((NMemoryLocalizationUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(IResourceRepository), (uow, log)
                => new NMemoryResourceRepository((NMemoryLocalizationUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(ITranslationRepository), (uow, log)
                => new NMemoryTranslationRepository((NMemoryLocalizationUnitOfWork) uow, log));
        }
    }
}