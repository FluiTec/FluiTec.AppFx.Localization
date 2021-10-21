using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Dapper.Repositories;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.Repositories
{
    /// <summary>
    /// A mssql translation repository.
    /// </summary>
    public class MssqlTranslationRepository : DapperTranslationRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public MssqlTranslationRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        /// <summary>
        /// Gets the 'get by languages' command.
        /// </summary>
        ///
        /// <value>
        /// The 'get by languages' command.
        /// </value>
        private string GetByLanguagesCommand
        {
            get
            {
                return GetFromCache(() =>
                        "SELECT * " +
                        $"FROM {SqlBuilder.Adapter.RenderTableName(typeof(TranslationEntity))} AS translation " +
                        $"LEFT JOIN {SqlBuilder.Adapter.RenderTableName(typeof(ResourceEntity))} AS tresource " +
                        "ON translation.ResourceId = tresource.Id " +
                        $"LEFT JOIN {SqlBuilder.Adapter.RenderTableName(typeof(LanguageEntity))} AS tlanguage " +
                        "ON translation.LanguageId = tlanguage.Id " +
                        $"LEFT JOIN {SqlBuilder.Adapter.RenderTableName(typeof(AuthorEntity))} AS author " +
                        "ON author.Id = tresource.AuthorId " +
                        $"WHERE {SqlBuilder.Adapter.RenderPropertyName(nameof(TranslationEntity.LanguageId))} " +
                        $"IN {SqlBuilder.Adapter.RenderParameterProperty("languageIds")}"
                    ,nameof(GetByLanguages), "languageIds");
            }
        }

        /// <summary>
        /// Gets the languages in this collection.
        /// </summary>
        ///
        /// <param name="languageIds">  List of identifiers for the languages. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the languages in this collection.
        /// </returns>
        public override IEnumerable<CompoundTranslationEntity> GetByLanguages(IEnumerable<int> languageIds)
        {
            var resourceDictionary = new Dictionary<int, ResourceEntity>();
            var languageDictionary = new Dictionary<int, LanguageEntity>();
            var authorDictionary = new Dictionary<int, AuthorEntity>();

            return UnitOfWork.Connection
                .Query<TranslationEntity, ResourceEntity, LanguageEntity, AuthorEntity, CompoundTranslationEntity>
                (
                    GetByLanguagesCommand,
                    (translation, resource, language, author) => new CompoundTranslationEntity
                    {
                        Translation = translation,
                        Resource = MapById(resourceDictionary, resource),
                        Language = MapById(languageDictionary, language),
                        Author = MapById(authorDictionary, author)
                    }, 
                    splitOn:nameof(IKeyEntity<int>.Id), 
                    transaction: UnitOfWork.Transaction, 
                    param: new { languageIds = languageIds.ToArray()}
                );
        }

        /// <summary>
        /// Gets by languages asynchronous.
        /// </summary>
        ///
        /// <param name="languageIds">  List of identifiers for the languages. </param>
        ///
        /// <returns>
        /// The by languages.
        /// </returns>
        public override Task<IEnumerable<CompoundTranslationEntity>> GetByLanguagesAsync(IEnumerable<int> languageIds)
        {
            var resourceDictionary = new Dictionary<int, ResourceEntity>();
            var languageDictionary = new Dictionary<int, LanguageEntity>();
            var authorDictionary = new Dictionary<int, AuthorEntity>();

            return UnitOfWork.Connection
                .QueryAsync<TranslationEntity, ResourceEntity, LanguageEntity, AuthorEntity, CompoundTranslationEntity>
                (
                    GetByLanguagesCommand,
                    (translation, resource, language, author) => new CompoundTranslationEntity
                    {
                        Translation = translation,
                        Resource = MapById(resourceDictionary, resource),
                        Language = MapById(languageDictionary, language),
                        Author = MapById(authorDictionary, author)
                    }, 
                    splitOn:nameof(IKeyEntity<int>.Id), 
                    transaction: UnitOfWork.Transaction, 
                    param: new { languageIds = languageIds.ToArray()}
                );
        }
    }
}