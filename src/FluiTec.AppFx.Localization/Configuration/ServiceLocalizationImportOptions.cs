using System.Collections.Generic;
using FluiTec.AppFx.Options.Attributes;

namespace FluiTec.AppFx.Localization.Configuration
{
    /// <summary>
    /// A service localization import options.
    /// </summary>
    [ConfigurationKey("ServiceLocalizationImportOptions")]
    public class ServiceLocalizationImportOptions
    {
        /// <summary>
        /// Gets or sets the default language ISO name.
        /// </summary>
        ///
        /// <value>
        /// The default language ISO name.
        /// </value>
        public string DefaultLanguageIsoName { get; set; }

        /// <summary>
        /// Gets or sets the updateable authors.
        /// </summary>
        ///
        /// <value>
        /// The updateable authors.
        /// </value>
        public List<string> UpdateableAuthors { get; set; }
    }
}