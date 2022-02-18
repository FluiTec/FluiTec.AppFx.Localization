using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Models;

namespace FluiTec.AppFx.Localization.Services
{
    /// <summary>
    /// Interface for translation picking service.
    /// </summary>
    public interface ITranslationPickingService
    {
        /// <summary>
        /// Picks.
        /// </summary>
        ///
        /// <param name="translations"> The translations. </param>
        /// <param name="culture">      The culture. </param>
        ///
        /// <returns>
        /// A CompoundTranslationEntity.
        /// </returns>
        CompoundTranslationEntity Pick(IList<CompoundTranslationEntity> translations, CultureInfo culture);

        /// <summary>
        /// Picks.
        /// </summary>
        ///
        /// <param name="translations"> The translations. </param>
        /// <param name="culture">      The culture. </param>
        /// <param name="sourceName">   Name of the source. </param>
        ///
        /// <returns>
        /// A CompoundTranslationEntity.
        /// </returns>
        LocalizedStringEx Pick(IList<CompoundTranslationEntity> translations, CultureInfo culture, string sourceName);

        /// <summary>
        /// Picks.
        /// </summary>
        ///
        /// <param name="groups">   The groups. </param>
        /// <param name="culture">  The culture. </param>
        ///
        /// <returns>
        /// A CompoundTranslationEntity.
        /// </returns>
        IEnumerable<CompoundTranslationEntity> Pick(IList<IGrouping<string, CompoundTranslationEntity>> groups, CultureInfo culture);

        /// <summary>
        /// Picks.
        /// </summary>
        ///
        /// <param name="groups">       The groups. </param>
        /// <param name="culture">      The culture. </param>
        /// <param name="sourceName">   Name of the source. </param>
        ///
        /// <returns>
        /// A CompoundTranslationEntity.
        /// </returns>
        IEnumerable<LocalizedStringEx> Pick(IList<IGrouping<string, CompoundTranslationEntity>> groups, CultureInfo culture, string sourceName);
    }
}