using FluiTec.AppFx.Localization.Reflection.Attributes;

namespace FluiTec.AppFx.Localization.Tests.Reflection.Models
{
    /// <summary>
    /// A data Model for the inheriting localized.
    /// </summary>
    [Localized]
    public class InheritingLocalizedModel : LocalizedModel
    {
        /// <summary>
        /// Gets or sets the name 2.
        /// </summary>
        ///
        /// <value>
        /// The name 2.
        /// </value>
        public string Name2 { get; set; }
    }
}