using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Data.LiteDb.DataServices;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb
{
    /// <summary>   A service for accessing lite database localization data information. </summary>
    public class LiteDbLocalizationDataService : LiteDbDataService<LiteDbLocalizationUnitOfWork>, ILocalizationDataService
    {
        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="useSingletonConnection">   The use singleton connection. </param>
        /// <param name="dbFilePath">               Full pathname of the database file. </param>
        /// <param name="loggerFactory">            The logger factory. </param>
        /// <param name="applicationFolder">        (Optional) Pathname of the application folder. </param>
        public LiteDbLocalizationDataService(bool? useSingletonConnection, string dbFilePath, ILoggerFactory loggerFactory, string applicationFolder = null) : base(useSingletonConnection, dbFilePath, loggerFactory, applicationFolder)
        {
        }

        /// <summary>   Constructor. </summary>
        /// <param name="useSingletonConnection">   The use singleton connection. </param>
        /// <param name="dbFilePath">               Full pathname of the database file. </param>
        /// <param name="loggerFactory">            The logger factory. </param>
        /// <param name="nameService">              The name service. </param>
        /// <param name="applicationFolder">        (Optional) Pathname of the application folder. </param>
        public LiteDbLocalizationDataService(bool? useSingletonConnection, string dbFilePath, ILoggerFactory loggerFactory, IEntityNameService nameService, string applicationFolder = null) : base(useSingletonConnection, dbFilePath, loggerFactory, nameService, applicationFolder)
        {
        }

        /// <summary>   Constructor. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        public LiteDbLocalizationDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory) : base(options, loggerFactory)
        {
        }

        /// <summary>   Constructor. </summary>
        /// <param name="options">          Options for controlling the operation. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        /// <param name="nameService">      The name service. </param>
        public LiteDbLocalizationDataService(LiteDbServiceOptions options, ILoggerFactory loggerFactory, IEntityNameService nameService) : base(options, loggerFactory, nameService)
        {
        }

        #endregion

        /// <summary>   Gets the name. </summary>
        /// <value> The name. </value>
        public override string Name => nameof(LiteDbLocalizationDataService);

        /// <summary>   Begins unit of work. </summary>
        /// <returns>   An IUnitOfWork. </returns>
        public override LiteDbLocalizationUnitOfWork BeginUnitOfWork()
        {
            return new LiteDbLocalizationUnitOfWork(this, LoggerFactory.CreateLogger<IUnitOfWork>());
        }

        /// <summary>   Begins unit of work. </summary>
        /// <param name="other">    The other. </param>
        /// <returns>   An IUnitOfWork. </returns>
        public override LiteDbLocalizationUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is LiteDbUnitOfWork))
                throw new ArgumentException($"Incompatible UnitOfWork. Must be of type {nameof(LiteDbUnitOfWork)}");
            return new LiteDbLocalizationUnitOfWork(this, (LiteDbUnitOfWork) other, LoggerFactory.CreateLogger<IUnitOfWork>());
        }

        /// <summary>   Begins unit of work. </summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        /// <exception cref="ArgumentException">        Thrown when one or more arguments have
        ///                                             unsupported or illegal values. </exception>
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