using System;
using System.Globalization;

namespace FluiTec.DbLocalizationProvider.Abstractions
{
    /// <summary>Attribute for validation translation for culture.</summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ValidationTranslationForCultureAttribute : Attribute
    {
        /// <summary>Obviously creates new instance of the attribute.</summary>
        /// <param name="validation">   The validation. </param>
        /// <param name="translation">  Translation of the resource for given language. </param>
        /// <param name="culture">
        ///     Language for the additional translation (will be used as argument
        ///     for <see cref="CultureInfo" />).
        /// </param>
        public ValidationTranslationForCultureAttribute(string validation, string translation, string culture)
        {
            Validation = validation;
            Translation = translation;
            Culture = culture;
        }

        /// <summary>Gets the validation.</summary>
        /// <value>The validation.</value>
        public string Validation { get; }

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
