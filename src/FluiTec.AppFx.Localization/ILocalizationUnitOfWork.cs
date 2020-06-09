using FluiTec.AppFx.Data.UnitsOfWork;
using FluiTec.AppFx.Localization.Repositories;

namespace FluiTec.AppFx.Localization
{
    /// <summary>Interface for localization unit of work.</summary>
    public interface ILocalizationUnitOfWork : IUnitOfWork
    {
        /// <summary>Gets or sets the resource repository.</summary>
        /// <value>The resource repository.</value>
        IResourceRepository ResourceRepository { get; }

        /// <summary>Gets or sets the translation repository.</summary>
        /// <value>The translation repository.</value>
        ITranslationRepository TranslationRepository { get; }
    }
}