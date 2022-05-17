using System;

namespace FluiTec.AppFx.Localization.Reflection.Attributes
{
    /// <summary>
    ///     Attribute for localized classes and enums.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class LocalizedAttribute : Attribute
    {
        /// <summary>
        ///     Gets or sets a value indicating whether the inherited properties should be considered a part of this class in terms
        ///     of localization.
        /// </summary>
        /// <value>
        ///     True if inherited, false if not.
        /// </value>
        public bool Inherited { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether only the manually included properties should be considered for
        ///     localization.
        /// </summary>
        /// <value>
        ///     True if only included, false if not.
        /// </value>
        public bool OnlyIncluded { get; set; } = false;
    }
}