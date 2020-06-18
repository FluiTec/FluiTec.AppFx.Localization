using System.Collections.Generic;
using FluiTec.AppFx.Options.Attributes;
using FluiTec.DbLocalizationProvider;

namespace FluiTec.AppFx.Localization.Configuration
{
    /// <summary>   A localization resources options. </summary>
    [ConfigurationKey("LocalizationResources")]
    public class LocalizationResourcesOptions
    {
        /// <summary>   Default constructor. </summary>
        public LocalizationResourcesOptions()
        {
            Resources = new List<LocalizationResource>();
        }

        /// <summary>   Gets or sets the resources. </summary>
        /// <value> The resources. </value>
        public List<LocalizationResource> Resources { get; set; }
    }
}
