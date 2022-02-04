using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>
    ///     Interface for language repository.
    /// </summary>
    public interface ILanguageRepository : IWritableKeyTableDataRepository<LanguageEntity, int>
    {
        /// <summary>
        ///     Gets a language entity using the given ISO name.
        /// </summary>
        /// <param name="isoName">  The ISO name to get. </param>
        /// <returns>
        ///     A LanguageEntity.
        /// </returns>
        LanguageEntity Get(string isoName);

        /// <summary>
        ///     Gets an asynchronous.
        /// </summary>
        /// <param name="isoName">  The ISO name to get. </param>
        /// <returns>
        ///     The asynchronous.
        /// </returns>
        Task<LanguageEntity> GetAsync(string isoName);

        /// <summary>
        ///     Gets the two letter isoes in this collection.
        /// </summary>
        /// <param name="cultureTwoLetterIsoLanguageName">  Name of the culture two letter ISO language. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the two letter isoes in this
        ///     collection.
        /// </returns>
        IEnumerable<LanguageEntity> GetByTwoLetterIso(string cultureTwoLetterIsoLanguageName);

        /// <summary>
        ///     Gets by two letter ISO asynchronous.
        /// </summary>
        /// <param name="cultureTwoLetterIsoLanguageName">  Name of the culture two letter ISO language. </param>
        /// <returns>
        ///     The by two letter ISO.
        /// </returns>
        Task<IEnumerable<LanguageEntity>> GetByTwoLetterIsoAsync(string cultureTwoLetterIsoLanguageName);
    }
}