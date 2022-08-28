using System;

namespace FluiTec.AppFx.Localization.Reflection.Attributes
{
    /// <summary>
    ///     Attribute for translation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = true)]
    public class TranslationAttribute : Attribute
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="languageIsoName">  The name of the language ISO. </param>
        /// <param name="value">            The value. </param>
        public TranslationAttribute(string languageIsoName, string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            LanguageIsoName = languageIsoName ?? throw new ArgumentNullException(nameof(languageIsoName));
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="languageIsoName">  The name of the language ISO. </param>
        /// <param name="value">            The value. </param>
        /// <param name="suffix">           The suffix. </param>
        public TranslationAttribute(string languageIsoName, string value, string suffix = null)
            : this(languageIsoName, value)
        {
            Suffix = suffix;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        ///     Gets or sets the name of the language ISO.
        /// </summary>
        /// <value>
        ///     The name of the language ISO.
        /// </value>
        public string LanguageIsoName { get; set; }

        /// <summary>
        ///     Gets or sets the suffix.
        /// </summary>
        /// <value>
        ///     The suffix.
        /// </value>
        public string Suffix { get; set; }
    }
}