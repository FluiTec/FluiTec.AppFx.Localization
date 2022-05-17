using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Models;

namespace FluiTec.AppFx.Localization.Services
{
    /// <summary>
    ///     A translation picking service using parent translations if necessary.
    /// </summary>
    public class ParentTranslationPickingService : ITranslationPickingService
    {
        /// <summary>
        ///     (Immutable) the culture ratings.
        /// </summary>
        private readonly Dictionary<CultureInfo, IList<KeyValuePair<int, string>>> _cultureRatings =
            new Dictionary<CultureInfo, IList<KeyValuePair<int, string>>>();

        /// <summary>
        ///     Picks.
        /// </summary>
        /// <param name="translations"> The translations. </param>
        /// <param name="culture">      The culture. </param>
        /// <returns>
        ///     A LocalizedStringEx.
        /// </returns>
        public CompoundTranslationEntity Pick(IList<CompoundTranslationEntity> translations, CultureInfo culture)
        {
            var ratings = GetCultureRating(culture);
            return translations
                .Where(t => ratings.Any(r => r.Value == t.Language.IsoName))
                .OrderByDescending(t => ratings.Single(r => r.Value == t.Language.IsoName).Key)
                .FirstOrDefault();
        }

        /// <summary>
        ///     Picks.
        /// </summary>
        /// <param name="translations"> The translations. </param>
        /// <param name="culture">      The culture. </param>
        /// <param name="sourceName">   Name of the source. </param>
        /// <returns>
        ///     A LocalizedStringEx.
        /// </returns>
        public LocalizedStringEx Pick(IList<CompoundTranslationEntity> translations, CultureInfo culture,
            string sourceName)
        {
            var entity = Pick(translations, culture);
            if (entity != null)
                return new LocalizedStringEx
                (
                    entity.Resource.ResourceKey,
                    entity.Translation.Value,
                    false,
                    sourceName,
                    entity.Language.IsoName
                );
            return null;
        }

        /// <summary>
        ///     Picks.
        /// </summary>
        /// <param name="groups">   The groups. </param>
        /// <param name="culture">  The culture. </param>
        /// <returns>
        ///     A LocalizedStringEx.
        /// </returns>
        public IEnumerable<CompoundTranslationEntity> Pick(IList<IGrouping<string, CompoundTranslationEntity>> groups,
            CultureInfo culture)
        {
            var ratings = GetCultureRating(culture);
            return groups
                .Select(g => g
                    .Where(t => ratings.Any(r => r.Value == t.Language.IsoName))
                    .OrderByDescending(t => ratings.Single(r => r.Value == t.Language.IsoName).Key)
                    .FirstOrDefault());
        }

        /// <summary>
        ///     Picks.
        /// </summary>
        /// <param name="groups">       The groups. </param>
        /// <param name="culture">      The culture. </param>
        /// <param name="sourceName">   Name of the source. </param>
        /// <returns>
        ///     A LocalizedStringEx.
        /// </returns>
        public IEnumerable<LocalizedStringEx> Pick(IList<IGrouping<string, CompoundTranslationEntity>> groups,
            CultureInfo culture, string sourceName)
        {
            return Pick(groups, culture)
                .Select(entity => new LocalizedStringEx
                (
                    entity.Resource.ResourceKey,
                    entity.Translation.Value,
                    false,
                    sourceName,
                    entity.Language.IsoName
                ));
        }

        /// <summary>
        ///     Gets the culture ratings in this collection.
        /// </summary>
        /// <param name="culture">  The culture. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the culture ratings in this
        ///     collection.
        /// </returns>
        protected IList<KeyValuePair<int, string>> GetCultureRating(CultureInfo culture)
        {
            if (_cultureRatings.ContainsKey(culture))
                return _cultureRatings[culture];

            var cultures = new List<CultureInfo>();
            var cCulture = culture;
            while (cCulture != null)
            {
                cultures.Add(cCulture);
                cCulture = !cCulture.Equals(cCulture.Parent) ? cCulture.Parent : null;
            }

            var strCultures = cultures
                .Select(c => !string.IsNullOrWhiteSpace(c.Name) ? c.Name : c.TwoLetterISOLanguageName)
                .Distinct();

            _cultureRatings.Add(culture, strCultures
                .Select((c, i) => new KeyValuePair<int, string>(i * -1, c))
                .ToList());
            return _cultureRatings[culture];
        }
    }
}