using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Schema;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper
{
    /// <summary>   A service for accessing dapper localization data information. </summary>
    public abstract class DapperLocalizationDataService : DapperDataService<DapperLocalizationUnitOfWork>,
        ILocalizationDataService
    {
        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        protected DapperLocalizationDataService(IDapperServiceOptions dapperServiceOptions,
            ILoggerFactory loggerFactory) : base(dapperServiceOptions, loggerFactory)
        {
        }

        /// <summary>   Gets the schema. </summary>
        /// <value> The schema. </value>
        public override string Schema => SchemaGlobals.Schema;

        /// <summary>   Begins unit of work. </summary>
        /// <param name="other">    The other. </param>
        /// <returns>   A TUnitOfWork. </returns>
        ILocalizationUnitOfWork IDataService<ILocalizationUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }

        /// <summary>   Begins unit of work. </summary>
        /// <returns>   A TUnitOfWork. </returns>
        ILocalizationUnitOfWork IDataService<ILocalizationUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }
    }
}