using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>
    /// Interface for language repository.
    /// </summary>
    public interface ILanguageRepository : IWritableKeyTableDataRepository<LanguageEntity, int>
    {
        /// <summary>
        /// Gets a language entity using the given ISO name.
        /// </summary>
        ///
        /// <param name="isoName">  The ISO name to get. </param>
        ///
        /// <returns>
        /// A LanguageEntity.
        /// </returns>
        LanguageEntity Get(string isoName);
    }
}