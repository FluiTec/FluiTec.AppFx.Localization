using System.Collections.Generic;
using FluiTec.AppFx.Localization.Import;
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
        /// Gets or sets the updateable authors.
        /// </summary>
        ///
        /// <value>
        /// The updateable authors.
        /// </value>
        public List<string> UpdateableAuthors { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ServiceLocalizationImportOptions()
        {
            UpdateableAuthors = new List<string>(new[] {CodeLocalizationSource.CodeAuthor});
        }
    }
}