using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.LiteDb.Repositories;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb
{
    /// <summary>
    ///     A lite database localization unit of work.
    /// </summary>
    public class LiteDbLocalizationUnitOfWork : LiteDbUnitOfWork, ILocalizationUnitOfWork
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbLocalizationUnitOfWork(ILiteDbDataService dataService, ILogger<IUnitOfWork> logger) : base(
            dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="dataService">      The data service. </param>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="logger">           The logger. </param>
        public LiteDbLocalizationUnitOfWork(ILiteDbDataService dataService, LiteDbUnitOfWork parentUnitOfWork,
            ILogger<IUnitOfWork> logger) : base(dataService, parentUnitOfWork, logger)
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
                => new LiteDbAuthorRepository((LiteDbLocalizationUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(ILanguageRepository), (uow, log)
                => new LiteDbLanguageRepository((LiteDbLocalizationUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(IResourceRepository), (uow, log)
                => new LiteDbResourceRepository((LiteDbLocalizationUnitOfWork) uow, log));
            RepositoryProviders.Add(typeof(ITranslationRepository), (uow, log)
                => new LiteDbTranslationRepository((LiteDbLocalizationUnitOfWork) uow, log));
        }
    }
}