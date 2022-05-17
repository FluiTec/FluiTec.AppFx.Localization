namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    ///     Interface for localization resource.
    /// </summary>
    public interface ILocalizationResource
    {
        /// <summary>
        ///     Gets the resource key.
        /// </summary>
        /// <value>
        ///     The resource key.
        /// </value>
        string ResourceKey { get; }

        /// <summary>
        ///     Gets the author.
        /// </summary>
        /// <value>
        ///     The author.
        /// </value>
        string Author { get; }

        /// <summary>
        ///     Gets the language.
        /// </summary>
        /// <value>
        ///     The language.
        /// </value>
        string Language { get; }

        /// <summary>
        ///     Gets the translation.
        /// </summary>
        /// <value>
        ///     The translation.
        /// </value>
        string Translation { get; }
    }
}