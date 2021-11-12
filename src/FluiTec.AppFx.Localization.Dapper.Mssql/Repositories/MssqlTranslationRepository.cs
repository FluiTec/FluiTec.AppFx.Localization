using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Dapper.Repositories;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.Repositories
{
    /// <summary>
    ///     A mssql translation repository.
    /// </summary>
    public class MssqlTranslationRepository : DapperTranslationRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public MssqlTranslationRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>
        ///     Gets the 'get by resource identifier' command.
        /// </summary>
        /// <value>
        ///     The 'get by resource identifier' command.
        /// </value>
        protected override string GetByResourceIdCommand
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
                    $"WHERE {SqlBuilder.Adapter.RenderPropertyName(nameof(TranslationEntity.ResourceId))} " +
                    $"= {SqlBuilder.Adapter.RenderParameterProperty("resourceId")}");
            }
        }

        /// <summary>
        ///     Gets the 'get by resource key' command.
        /// </summary>
        /// <value>
        ///     The 'get by resource key' command.
        /// </value>
        protected override string GetByResourceKeyCommand
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
                    $"WHERE {SqlBuilder.Adapter.RenderPropertyName(nameof(ResourceEntity.ResourceKey))} " +
                    $"= {SqlBuilder.Adapter.RenderParameterProperty("resourceKey")}");
            }
        }

        /// <summary>
        ///     Gets the 'get by languages' command.
        /// </summary>
        /// <value>
        ///     The 'get by languages' command.
        /// </value>
        protected override string GetByLanguagesCommand
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
                    , nameof(GetByLanguages), "languageIds");
            }
        }

        /// <summary>
        ///     Gets the 'get by resource suffix' command.
        /// </summary>
        /// <value>
        ///     The 'get by resource suffix' command.
        /// </value>
        protected override string GetByResourceSuffixCommand
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
                    $"WHERE {SqlBuilder.Adapter.RenderPropertyName(nameof(ResourceEntity.ResourceKey))} " +
                    $"LIKE {SqlBuilder.Adapter.RenderParameterProperty("suffix")} + '%'");
            }
        }
    }
}