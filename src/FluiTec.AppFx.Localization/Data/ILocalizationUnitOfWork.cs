using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Repositories;

namespace FluiTec.AppFx.Localization
{
    /// <summary>
    ///     Interface for localization unit of work.
    /// </summary>
    public interface ILocalizationUnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     Gets the author repository.
        /// </summary>
        /// <value>
        ///     The author repository.
        /// </value>
        IAuthorRepository AuthorRepository { get; }

        /// <summary>
        ///     Gets the language repository.
        /// </summary>
        /// <value>
        ///     The language repository.
        /// </value>
        ILanguageRepository LanguageRepository { get; }

        /// <summary>
        ///     Gets the resource repository.
        /// </summary>
        /// <value>
        ///     The resource repository.
        /// </value>
        IResourceRepository ResourceRepository { get; }

        /// <summary>
        ///     Gets the translation repository.
        /// </summary>
        /// <value>
        ///     The translation repository.
        /// </value>
        ITranslationRepository TranslationRepository { get; }
    }
}