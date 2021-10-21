using System.Collections.Generic;
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
        /// Gets an asynchronous.
        /// </summary>
        ///
        /// <param name="isoName">  The ISO name to get. </param>
        ///
        /// <returns>
        /// The asynchronous.
        /// </returns>
        public Task<LanguageEntity> GetAsync(string isoName)
        {
            return Task.FromResult(Get(isoName));
        }

        /// <summary>
        /// Gets the two letter isoes in this collection.
        /// </summary>
        ///
        /// <param name="cultureTwoLetterIsoLanguageName">  Name of the culture two letter ISO language. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the two letter isoes in this
        /// collection.
        /// </returns>
        public IEnumerable<LanguageEntity> GetByTwoLetterIso(string cultureTwoLetterIsoLanguageName)
        {
            return Table.Where(e => e.IsoName.StartsWith(cultureTwoLetterIsoLanguageName));
        }

        /// <summary>
        /// Gets by two letter ISO asynchronous.
        /// </summary>
        ///
        /// <param name="cultureTwoLetterIsoLanguageName">  Name of the culture two letter ISO language. </param>
        ///
        /// <returns>
        /// The by two letter ISO.
        /// </returns>
        public Task<IEnumerable<LanguageEntity>> GetByTwoLetterIsoAsync(string cultureTwoLetterIsoLanguageName)
        {
            return Task.FromResult(GetByTwoLetterIso(cultureTwoLetterIsoLanguageName));
        }
    }
}