namespace FluiTec.AppFx.Localization.Entities
{
    /// <summary>
    /// A compound translation entity.
    /// </summary>
    public class CompoundTranslationEntity
    {
        /// <summary>
        /// Gets or sets the translation.
        /// </summary>
        ///
        /// <value>
        /// The translation.
        /// </value>
        public TranslationEntity Translation { get; set; }

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        ///
        /// <value>
        /// The resource.
        /// </value>
        public ResourceEntity Resource { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        ///
        /// <value>
        /// The language.
        /// </value>
        public LanguageEntity Language { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        ///
        /// <value>
        /// The author.
        /// </value>
        public AuthorEntity Author { get; set; }
    }
}