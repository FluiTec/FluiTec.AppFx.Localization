using System.Collections.Generic;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    /// A file localization resource.
    /// </summary>
    public class FileLocalizationResource
    {
        /// <summary>
        /// Gets or sets the resource key.
        /// </summary>
        ///
        /// <value>
        /// The resource key.
        /// </value>
        public string ResourceKey { get; set; }

        /// <summary>
        /// Gets or sets the translations.
        /// </summary>
        ///
        /// <value>
        /// The translations.
        /// </value>
        public List<FileLocalizationTranslation> Translations { get; set; }
    }
}