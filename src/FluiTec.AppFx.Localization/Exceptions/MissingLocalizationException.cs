using System;
using System.Globalization;

namespace FluiTec.AppFx.Localization.Exceptions
{
    /// <summary>
    /// Exception for signalling missing localization errors.
    /// </summary>
    public class MissingLocalizationException : Exception
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the culture.
        /// </summary>
        ///
        /// <value>
        /// The culture.
        /// </value>
        public CultureInfo Culture { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="name">     The name. </param>
        /// <param name="culture">  The culture. </param>
        public MissingLocalizationException(string name, CultureInfo culture)
        {
            Name = name;
            Culture = culture;
        }
    }
}