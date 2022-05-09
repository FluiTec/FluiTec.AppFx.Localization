
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.Models
{
    /// <summary>
    /// Exception for signalling localized string errors.
    /// </summary>
    public class LocalizedStringEx : LocalizedString
    {
        /// <summary>
        /// Gets the language.
        /// </summary>
        ///
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="name">             The name. </param>
        /// <param name="value">            The value. </param>
        /// <param name="resourceNotFound"> True to resource not found. </param>
        /// <param name="searchedLocation"> The searched location. </param>
        /// <param name="language">         The language. </param>
        public LocalizedStringEx(string name, string value, bool resourceNotFound, string searchedLocation, string language) : base(name, value, resourceNotFound, searchedLocation)
        {
            Language = language;
        }
    }
}
