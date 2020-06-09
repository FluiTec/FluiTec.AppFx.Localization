using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.LiteDb.Repositories;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb
{
    /// <summary>   A lite database localization unit of work. </summary>
    public class LiteDbLocalizationUnitOfWork : LiteDbUnitOfWork, ILocalizationUnitOfWork
    {
        /// <summary>   Constructor. </summary>
        /// <param name="dataService">  The data service. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbLocalizationUnitOfWork(ILiteDbDataService dataService, ILogger<IUnitOfWork> logger) : base(dataService, logger)
        {
            RegisterRepositories();
        }

        /// <summary>   Constructor. </summary>
        /// <param name="dataService">      The data service. </param>
        /// <param name="parentUnitOfWork"> The parent unit of work. </param>
        /// <param name="logger">           The logger. </param>
        public LiteDbLocalizationUnitOfWork(ILiteDbDataService dataService, LiteDbUnitOfWork parentUnitOfWork, ILogger<IUnitOfWork> logger) : base(dataService, parentUnitOfWork, logger)
        {
            RegisterRepositories();
        }

        /// <summary>   Registers the repositories. </summary>
        private void RegisterRepositories()
        {
            RepositoryProviders.Add(typeof(IResourceRepository), (uow, logger) => new LiteDbResourceRepository((LiteDbUnitOfWork)uow, logger));
            RepositoryProviders.Add(typeof(ITranslationRepository), (uow, logger) => new LiteDbTranslationRepository((LiteDbUnitOfWork)uow, logger));
        }

        /// <summary>   Gets the resource repository. </summary>
        /// <value> The resource repository. </value>
        public IResourceRepository ResourceRepository => GetRepository<IResourceRepository>();

        /// <summary>   Gets the translation repository. </summary>
        /// <value> The translation repository. </value>
        public ITranslationRepository TranslationRepository => GetRepository<ITranslationRepository>();
    }
}