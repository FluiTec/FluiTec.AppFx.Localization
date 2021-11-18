using FluiTec.AppFx.Localization.Reflection.Attributes;

namespace FluiTec.AppFx.Localization.Tests.Reflection.Models
{
    /// <summary>
    /// A data Model for the localized.
    /// </summary>
    [Localized]
    public class LocalizedModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}