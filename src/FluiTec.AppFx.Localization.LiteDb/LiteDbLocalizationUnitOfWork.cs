using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb
{
    /// <summary>   A lite database localization unit of work. </summary>
    public class LiteDbLocalizationUnitOfWork : LiteDbUnitOfWork
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

        }
    }
}