using System.Threading.Tasks;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    ///     Interface for localization import service.
    /// </summary>
    public interface ILocalizationImportService
    {
        /// <summary>
        ///     Gets the import.
        /// </summary>
        /// <returns>
        ///     An int.
        /// </returns>
        int Import();

        /// <summary>
        ///     Import asynchronous.
        /// </summary>
        /// <returns>
        ///     The import.
        /// </returns>
        Task<int> ImportAsync();
    }
}