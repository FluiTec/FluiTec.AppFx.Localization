using System.Threading.Tasks;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb.Repositories
{
    /// <summary>
    ///     A lite database language repository.
    /// </summary>
    public class LiteDbLanguageRepository : LiteDbWritableIntegerKeyTableDataRepository<LanguageEntity>,
        ILanguageRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbLanguageRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>
        ///     Gets a language entity using the given ISO name.
        /// </summary>
        /// <param name="isoName">  The ISO name to get. </param>
        /// <returns>
        ///     A LanguageEntity.
        /// </returns>
        public LanguageEntity Get(string isoName)
        {
            return Collection.FindOne(entity => entity.IsoName == isoName);
        }

        /// <summary>
        ///     Gets an asynchronous.
        /// </summary>
        /// <param name="isoName">  The ISO name to get. </param>
        /// <returns>
        ///     The asynchronous.
        /// </returns>
        public Task<LanguageEntity> GetAsync(string isoName)
        {
            return Task.FromResult(Get(isoName));
        }
    }
}