using System;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Localization.Dapper.Mssql
{
    /// <summary>
    /// A service for accessing mssql localization data information.
    /// </summary>
    public class MssqlLocalizationDataService : DapperLocalizationDataService
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        public MssqlLocalizationDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) : base(dapperServiceOptions, loggerFactory)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        public MssqlLocalizationDataService(IOptionsMonitor<IDapperServiceOptions> dapperServiceOptions, ILoggerFactory loggerFactory) : base(dapperServiceOptions, loggerFactory)
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        public override string Name => nameof(MssqlLocalizationDataService);

        /// <summary>
        /// Gets the schema.
        /// </summary>
        ///
        /// <value>
        /// The schema.
        /// </value>
        public override string Schema => SchemaGlobals.Schema;

        /// <summary>
        /// Gets the type of the SQL.
        /// </summary>
        ///
        /// <value>
        /// The type of the SQL.
        /// </value>
        public override SqlType SqlType => SqlType.Mssql;

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        ///
        /// <returns>
        /// An IUnitOfWork.
        /// </returns>
        public override DapperLocalizationUnitOfWork BeginUnitOfWork()
        {
            return new MssqlLocalizationUnitOfWork(this, LoggerFactory.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        /// Begins unit of work.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <exception cref="ArgumentException">        Thrown when one or more arguments have
        ///                                             unsupported or illegal values. </exception>
        ///
        /// <param name="other">    The other. </param>
        ///
        /// <returns>
        /// An IUnitOfWork.
        /// </returns>
        public override DapperLocalizationUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is DapperUnitOfWork work))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(DapperUnitOfWork)}!");
            return new MssqlLocalizationUnitOfWork(work, this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

    }
}