using System.Collections.Generic;
using System.Globalization;
using FluiTec.AppFx.Localization.Exceptions;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.Localizers
{
    /// <summary>
    /// A fallback string localizer.
    /// </summary>
    public class FallbackStringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// (Immutable) the origina localizer.
        /// </summary>
        private readonly IStringLocalizer _originaLocalizer;

        /// <summary>
        /// (Immutable) the fallback localizer.
        /// </summary>
        private readonly IStringLocalizer _fallbackLocalizer;

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="originaLocalizer">     The origina localizer. </param>
        /// <param name="fallbackLocalizer">    The fallback localizer. </param>
        public FallbackStringLocalizer(IStringLocalizer originaLocalizer, IStringLocalizer fallbackLocalizer)
        {
            _originaLocalizer = originaLocalizer;
            _fallbackLocalizer = fallbackLocalizer;
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _originaLocalizer.GetAllStrings(includeParentCultures);
        }

        /// <summary>
        /// Creates a new <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> for a
        /// specific <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        ///
        /// <param name="culture">  The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        ///
        /// <returns>
        /// A culture-specific <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return _originaLocalizer.WithCulture(culture);
        }

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        ///
        /// <param name="name"> The name of the string resource. </param>
        ///
        /// <returns>
        /// The string resource as a <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        public LocalizedString this[string name]
        {
            get
            {
                try
                {
                    var result = _originaLocalizer[name];
                    return result.ResourceNotFound ? _fallbackLocalizer[name] : result;
                }
                catch (MissingLocalizationException)
                {
                    return _fallbackLocalizer[name];
                }
            }
        }

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        ///
        /// <param name="name">         The name of the string resource. </param>
        /// <param name="arguments">    A variable-length parameters list containing arguments. </param>
        ///
        /// <returns>
        /// The string resource as a <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                try
                {
                    var result = _originaLocalizer[name, arguments];
                    return result.ResourceNotFound 
                        ? _fallbackLocalizer[name, arguments] 
                        : result;
                }
                catch (MissingLocalizationException)
                {
                    return _fallbackLocalizer[name, arguments];
                }
            }
        }
    }
}