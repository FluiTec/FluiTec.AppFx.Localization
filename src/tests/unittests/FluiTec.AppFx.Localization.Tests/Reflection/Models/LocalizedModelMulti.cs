using FluiTec.AppFx.Localization.Reflection.Attributes;

namespace FluiTec.AppFx.Localization.Tests.Reflection.Models
{
    /// <summary>
    /// A localized model multi.
    /// </summary>
    [Localized]
    public class LocalizedModelMulti
    {
        /// <summary>
        /// Gets or sets the name 1.
        /// </summary>
        ///
        /// <value>
        /// The name 1.
        /// </value>
        public string Name1 { get; set; }

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