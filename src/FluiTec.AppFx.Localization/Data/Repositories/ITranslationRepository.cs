using System.Collections.Generic;
using System.Threading.Tasks;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>
    ///     Interface for translation repository.
    /// </summary>
    public interface ITranslationRepository : IWritableKeyTableDataRepository<TranslationEntity, int>
    {
        /// <summary>
        ///     Gets the resources in this collection.
        /// </summary>
        /// <param name="resource"> The resource. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByResource(ResourceEntity resource);

        /// <summary>
        ///     Gets the resource compounds in this collection.
        /// </summary>
        /// <param name="resource"> The resource. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resource compounds in this
        ///     collection.
        /// </returns>
        IEnumerable<CompoundTranslationEntity> GetByResourceCompound(ResourceEntity resource);

        /// <summary>
        ///     Gets by resource asynchronous.
        /// </summary>
        /// <param name="resource"> The resource. </param>
        /// <returns>
        ///     The by resource.
        /// </returns>
        Task<IEnumerable<TranslationEntity>> GetByResourceAsync(ResourceEntity resource);

        /// <summary>
        ///     Gets by resource compound asynchronous.
        /// </summary>
        /// <param name="resource"> The resource. </param>
        /// <returns>
        ///     The by resource compound.
        /// </returns>
        Task<IEnumerable<CompoundTranslationEntity>> GetByResourceCompoundAsync(ResourceEntity resource);

        /// <summary>
        ///     Gets the resources in this collection.
        /// </summary>
        /// <param name="resourceId">   Identifier for the resource. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByResource(int resourceId);

        /// <summary>
        ///     Gets the resource compounds in this collection.
        /// </summary>
        /// <param name="resourceId">   Identifier for the resource. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resource compounds in this
        ///     collection.
        /// </returns>
        IEnumerable<CompoundTranslationEntity> GetByResourceCompound(int resourceId);

        /// <summary>
        ///     Gets by resource asynchronous.
        /// </summary>
        /// <param name="resourceId">   Identifier for the resource. </param>
        /// <returns>
        ///     The by resource.
        /// </returns>
        Task<IEnumerable<TranslationEntity>> GetByResourceAsync(int resourceId);

        /// <summary>
        ///     Gets by resource compound asynchronous.
        /// </summary>
        /// <param name="resourceId">   Identifier for the resource. </param>
        /// <returns>
        ///     The by resource compound.
        /// </returns>
        Task<IEnumerable<CompoundTranslationEntity>> GetByResourceCompoundAsync(int resourceId);

        /// <summary>
        ///     Gets the resources in this collection.
        /// </summary>
        /// <param name="resourceKey">  The resource key. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByResource(string resourceKey);

        /// <summary>
        ///     Gets the resource compounds in this collection.
        /// </summary>
        /// <param name="resourceKey">  The resource key. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resource compounds in this
        ///     collection.
        /// </returns>
        IEnumerable<CompoundTranslationEntity> GetByResourceCompound(string resourceKey);

        /// <summary>
        ///     Gets by resource asynchronous.
        /// </summary>
        /// <param name="resourceKey">  The resource key. </param>
        /// <returns>
        ///     The by resource.
        /// </returns>
        Task<IEnumerable<TranslationEntity>> GetByResourceAsync(string resourceKey);

        /// <summary>
        ///     Gets by resource compound asynchronous.
        /// </summary>
        /// <param name="resourceKey">   The resource key. </param>
        /// <returns>
        ///     The by resource compound.
        /// </returns>
        Task<IEnumerable<CompoundTranslationEntity>> GetByResourceCompoundAsync(string resourceKey);

        /// <summary>
        ///     Gets the languages in this collection.
        /// </summary>
        /// <param name="language"> The language. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByLanguage(LanguageEntity language);

        /// <summary>
        ///     Gets by language asynchronous.
        /// </summary>
        /// <param name="language"> The language. </param>
        /// <returns>
        ///     The by language.
        /// </returns>
        Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(LanguageEntity language);

        /// <summary>
        ///     Gets the languages in this collection.
        /// </summary>
        /// <param name="languageId">   Identifier for the language. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByLanguage(int languageId);

        /// <summary>
        ///     Gets by language asynchronous.
        /// </summary>
        /// <param name="languageId">   Identifier for the language. </param>
        /// <returns>
        ///     The by language.
        /// </returns>
        Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(int languageId);

        /// <summary>
        ///     Gets the languages in this collection.
        /// </summary>
        /// <param name="isoName">  Name of the ISO. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> GetByLanguage(string isoName);

        /// <summary>
        ///     Gets by language asynchronous.
        /// </summary>
        /// <param name="isoName">  Name of the ISO. </param>
        /// <returns>
        ///     The by language.
        /// </returns>
        Task<IEnumerable<TranslationEntity>> GetByLanguageAsync(string isoName);

        /// <summary>
        ///     Gets the languages in this collection.
        /// </summary>
        /// <param name="languages">    The languages. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        IEnumerable<CompoundTranslationEntity> GetByLanguages(IEnumerable<LanguageEntity> languages);

        /// <summary>
        ///     Gets by languages asynchronous.
        /// </summary>
        /// <param name="languages">    The languages. </param>
        /// <returns>
        ///     The by languages.
        /// </returns>
        Task<IEnumerable<CompoundTranslationEntity>> GetByLanguagesAsync(IEnumerable<LanguageEntity> languages);

        /// <summary>
        ///     Gets the languages in this collection.
        /// </summary>
        /// <param name="languageIds">  List of identifiers for the languages. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        IEnumerable<CompoundTranslationEntity> GetByLanguages(IEnumerable<int> languageIds);

        /// <summary>
        ///     Gets by languages asynchronous.
        /// </summary>
        /// <param name="languageIds">  List of identifiers for the languages. </param>
        /// <returns>
        ///     The by languages.
        /// </returns>
        Task<IEnumerable<CompoundTranslationEntity>> GetByLanguagesAsync(IEnumerable<int> languageIds);

        /// <summary>
        ///     Gets the resource suffix compounds in this collection.
        /// </summary>
        /// <param name="suffix">   The suffix. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resource suffix compounds in this
        ///     collection.
        /// </returns>
        IEnumerable<CompoundTranslationEntity> GetByResourceSuffixCompound(string suffix);

        /// <summary>
        ///     Gets by resource suffix compound asynchronous.
        /// </summary>
        /// <param name="suffix">   The suffix. </param>
        /// <returns>
        ///     The by resource suffix compound.
        /// </returns>
        Task<IEnumerable<CompoundTranslationEntity>> GetByResourceSuffixCompoundAsync(string suffix);
    }
}