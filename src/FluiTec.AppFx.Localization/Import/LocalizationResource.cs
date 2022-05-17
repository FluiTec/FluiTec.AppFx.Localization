namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    ///     A localization resource.
    /// </summary>
    public class LocalizationResource : ILocalizationResource
    {
        /// <summary>
        ///     Gets or sets the resource key.
        /// </summary>
        /// <value>
        ///     The resource key.
        /// </value>
        public string ResourceKey { get; set; }

        /// <summary>
        ///     Gets or sets the author.
        /// </summary>
        /// <value>
        ///     The author.
        /// </value>
        public string Author { get; set; }

        /// <summary>
        ///     Gets or sets the language.
        /// </summary>
        /// <value>
        ///     The language.
        /// </value>
        public string Language { get; set; }

        /// <summary>
        ///     Gets or sets the translation.
        /// </summary>
        /// <value>
        ///     The translation.
        /// </value>
        public string Translation { get; set; }
    }
}