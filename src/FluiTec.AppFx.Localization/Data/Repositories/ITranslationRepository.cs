using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>
    /// Interface for translation repository.
    /// </summary>
    public interface ITranslationRepository : IWritableKeyTableDataRepository<TranslationEntity, int>
    {
        /// <summary>
        /// Gets the resources in this collection.
        /// </summary>
        ///
        /// <param name="resource"> The resource. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByResource(ResourceEntity resource);

        /// <summary>
        /// Gets the resources in this collection.
        /// </summary>
        ///
        /// <param name="resourceId">   Identifier for the resource. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByResource(int resourceId);

        /// <summary>
        /// Gets the languages in this collection.
        /// </summary>
        ///
        /// <param name="language"> The language. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByLanguage(LanguageEntity language);

        /// <summary>
        /// Gets the languages in this collection.
        /// </summary>
        ///
        /// <param name="languageId">   Identifier for the language. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByLanguage(int languageId);
    }
}