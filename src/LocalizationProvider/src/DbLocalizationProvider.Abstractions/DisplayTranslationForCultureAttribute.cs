using System;
using System.Globalization;

namespace FluiTec.DbLocalizationProvider.Abstractions
{
    /// <summary>Attribute for display translation for culture.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class DisplayTranslationForCultureAttribute : Attribute
    {
        /// <summary>   Obviously creates new instance of the attribute. </summary>
        /// <param name="display">      The display-name. </param>
        /// <param name="translation">  Translation of the resource for given language. </param>
        /// <param name="culture">
        ///     Language for the additional translation (will be used as argument
        ///     for <see cref="CultureInfo" />).
        /// </param>
        public DisplayTranslationForCultureAttribute(string display, string translation, string culture)
        {
            Display = display;
            Translation = translation;
            Culture = culture;
        }

        /// <summary>Gets the display.</summary>
        /// <value>The display.</value>
        public string Display { get; }

        /// <summary>
        ///     Translation of the resource for given language.
        /// </summary>
        public string Translation { get; }

        /// <summary>
        ///     Language for the additional translation (will be used as argument for <see cref="CultureInfo" />).
        /// </summary>
        public string Culture { get; }
    }
}
