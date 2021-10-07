using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.NMemory.Repositories;
using FluiTec.AppFx.Data.NMemory.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.NMemory.Repositories
{
    /// <summary>
    ///     A memory language repository.
    /// </summary>
    public class NMemoryLanguageRepository : NMemoryWritableKeyTableDataRepository<LanguageEntity, int>,
        ILanguageRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public NMemoryLanguageRepository(NMemoryUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
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
            return Table.SingleOrDefault(e => e.IsoName == isoName);
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