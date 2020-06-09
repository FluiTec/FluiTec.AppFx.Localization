using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.DbLocalizationProvider;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization
{
    /// <summary>   A database string localizer. </summary>
    public class DbStringLocalizer : IStringLocalizer
    {
        /// <summary>   The culture. </summary>
        private readonly CultureInfo _culture;

        /// <summary>   Default constructor. </summary>
        public DbStringLocalizer()
        {
            _culture = CultureInfo.CurrentUICulture;
        }

        /// <summary>   Constructor. </summary>
        /// <param name="culture">  The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        public DbStringLocalizer(CultureInfo culture) : this()
        {
            _culture = culture;
        }

        /// <summary>   Gets all string resources. </summary>
        /// <param name="includeParentCultures">
        ///     A <see cref="T:System.Boolean" /> indicating whether
        ///     to include strings from parent cultures.
        /// </param>
        /// <returns>   The strings. </returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return Enumerable.Empty<LocalizedString>();
        }

        /// <summary>
        ///     Creates a new <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> for a
        ///     specific <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <param name="culture">  The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        /// <returns>
        ///     A culture-specific <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new DbStringLocalizer(culture);
        }

        /// <summary>   Gets the string resource with the given name. </summary>
        /// <param name="name"> The name of the string resource. </param>
        /// <returns>
        ///     The string resource as a
        ///     <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        LocalizedString IStringLocalizer.this[string name]
        {
            get
            {
                var value = LocalizationProvider.Current.GetStringByCulture(name, _culture);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }

        /// <summary>   Gets the string resource with the given name. </summary>
        /// <param name="name">         The name of the string resource. </param>
        /// <param name="arguments">    A variable-length parameters list containing arguments. </param>
        /// <returns>
        ///     The string resource as a
        ///     <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        LocalizedString IStringLocalizer.this[string name, params object[] arguments]
        {
            get
            {
                var value = LocalizationProvider.Current.GetStringByCulture(name, _culture, arguments);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }
    }
}