using FluiTec.AppFx.Localization.Reflection.Attributes;

namespace FluiTec.AppFx.Localization.Tests.Reflection.Models
{
    /// <summary>
    ///     A data Model for the only included localized.
    /// </summary>
    [Localized(OnlyIncluded = true)]
    public class OnlyIncludedLocalizedModel
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the name 2.
        /// </summary>
        /// <value>
        ///     The name 2.
        /// </value>
        [Include]
        public string Name2 { get; set; }
    }

    /// <summary>
    ///     A data Model for the inheriting only included localized.
    /// </summary>
    [Localized(OnlyIncluded = true)]
    public class InheritingOnlyIncludedLocalizedModel : OnlyIncludedLocalizedModel
    {
        /// <summary>
        ///     Gets or sets the name 3.
        /// </summary>
        /// <value>
        ///     The name 3.
        /// </value>
        [Include]
        public string Name3 { get; set; }
    }

    /// <summary>
    ///     A data Model for the non inheriting only included localized.
    /// </summary>
    [Localized(Inherited = false, OnlyIncluded = true)]
    public class NonInheritingOnlyIncludedLocalizedModel : OnlyIncludedLocalizedModel
    {
        /// <summary>
        ///     Gets or sets the name 3.
        /// </summary>
        /// <value>
        ///     The name 3.
        /// </value>
        [Include]
        public string Name3 { get; set; }
    }
}