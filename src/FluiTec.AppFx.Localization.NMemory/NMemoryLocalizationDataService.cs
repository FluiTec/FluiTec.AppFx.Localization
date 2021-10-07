using System;
using FluiTec.AppFx.Data.DataServices;
using FluiTec.AppFx.Data.NMemory.DataServices;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.Extensions.Logging;
using NMemory;
using NMemory.Tables;

namespace FluiTec.AppFx.Localization.NMemory
{
    /// <summary>
    ///     A service for accessing memory localization data information.
    /// </summary>
    public class NMemoryLocalizationDataService : NMemoryDataService<NMemoryLocalizationUnitOfWork>,
        ILocalizationDataService
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="loggerFactory">    The logger factory. </param>
        public NMemoryLocalizationDataService(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        /// <summary>
        ///     Gets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public override string Name => nameof(NMemoryLocalizationDataService);

        /// <summary>
        ///     Begins unit of work.
        /// </summary>
        /// <param name="other">    The other. </param>
        /// <returns>
        ///     A TUnitOfWork.
        /// </returns>
        ILocalizationUnitOfWork IDataService<ILocalizationUnitOfWork>.BeginUnitOfWork(IUnitOfWork other)
        {
            return BeginUnitOfWork(other);
        }

        /// <summary>
        ///     Begins unit of work.
        /// </summary>
        /// <returns>
        ///     A TUnitOfWork.
        /// </returns>
        ILocalizationUnitOfWork IDataService<ILocalizationUnitOfWork>.BeginUnitOfWork()
        {
            return BeginUnitOfWork();
        }

        /// <summary>
        ///     Begins unit of work.
        /// </summary>
        /// <returns>
        ///     An IUnitOfWork.
        /// </returns>
        public override NMemoryLocalizationUnitOfWork BeginUnitOfWork()
        {
            return new NMemoryLocalizationUnitOfWork(this, LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        ///     Begins unit of work.
        /// </summary>
        /// <param name="other">    The other. </param>
        /// <returns>
        ///     An IUnitOfWork.
        /// </returns>
        public override NMemoryLocalizationUnitOfWork BeginUnitOfWork(IUnitOfWork other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (!(other is NMemoryUnitOfWork work))
                throw new ArgumentException(
                    $"Incompatible implementation of UnitOfWork. Must be of type {nameof(NMemoryUnitOfWork)}!");
            return new NMemoryLocalizationUnitOfWork(work, work.DataService,
                LoggerFactory?.CreateLogger<IUnitOfWork>());
        }

        /// <summary>
        ///     Configure database.
        /// </summary>
        /// <param name="database"> The database. </param>
        /// <returns>
        ///     A Database.
        /// </returns>
        protected override Database ConfigureDatabase(Database database)
        {
            database.Tables.Create(e => e.Id, new IdentitySpecification<AuthorEntity>(e => e.Id));
            database.Tables.Create(e => e.Id, new IdentitySpecification<LanguageEntity>(e => e.Id));
            database.Tables.Create(e => e.Id, new IdentitySpecification<ResourceEntity>(e => e.Id));
            database.Tables.Create(e => e.Id, new IdentitySpecification<TranslationEntity>(e => e.Id));
            return database;
        }
    }
}