using System.Collections.Generic;
using System.Globalization;
using FluiTec.AppFx.Localization.Models;

namespace FluiTec.AppFx.Localization.Services
{
    /// <summary>
    ///     Interface for localization service.
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        ///     By name.
        /// </summary>
        /// <param name="name">     The name. </param>
        /// <param name="culture">  The culture. </param>
        /// <returns>
        ///     A LocalizedString.
        /// </returns>
        LocalizedStringEx ByName(string name, CultureInfo culture);

        /// <summary>
        /// By name not found.
        /// </summary>
        ///
        /// <param name="name">     The name. </param>
        /// <param name="culture">  The culture. </param>
        ///
        /// <returns>
        /// A LocalizedStringEx.
        /// </returns>
        LocalizedStringEx ByNameNotFound(string name, CultureInfo culture);
        
        /// <summary>
        ///     Enumerates by base name in this collection.
        /// </summary>
        /// <param name="baseName"> Name of the base. </param>
        /// <param name="culture">  The culture. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process by base name in this collection.
        /// </returns>
        IEnumerable<LocalizedStringEx> ByBaseName(string baseName, CultureInfo culture);
    }
}