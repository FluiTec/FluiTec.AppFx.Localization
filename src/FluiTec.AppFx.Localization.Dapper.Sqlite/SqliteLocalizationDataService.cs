using System;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Sqlite
{
    /// <summary>   A service for accessing Sqlite localization data information. </summary>
    public class SqliteLocalizationDataService : DapperLocalizationDataService
    {
        /// <summary>   Constructor. </summary>
        /// <param name="dapperServiceOptions"> Options for controlling the dapper service. </param>
        /// <param name="loggerFactory">        The logger factory. </param>
        public SqliteLocalizationDataService(IDapperServiceOptions dapperServiceOptions, ILoggerFactory loggerFactory) :
            base(dapperServiceOptions, loggerFactory)
        {
        }

        /// <summary>   Gets the name. </summary>
        /// <value> The name. </value>
        public override string Name => nameof(SqliteLocalizationDataService);

        /// <summary>   Gets the type of the SQL. </summary>
        /// <value> The type of the SQL. </value>
        public override SqlType SqlType => SqlType.Sqlite;

        /// <summary>   Begins unit of work. </summary>
        /// <returns>   An IUnitOfWork. </returns>
        public override DapperLocalizationUnitOfWork BeginUnitOfWork()
        {
            return new SqliteLocalizationUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>   Begins unit of work. </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have
        ///     unsupported or illegal values.
        /// </exception>
        /// <param name="other">    The other. </param>
        /// <returns>   An IUnitOfWork. </returns>
        public override DapperLocalizationUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is DapperUnitOfWork))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(DapperUnitOfWork)}!");
            return new SqliteLocalizationUnitOfWork((DapperUnitOfWork) other, this,
                LoggerFactory?.CreateLogger<IUnitOfWork>());
        }
    }
}