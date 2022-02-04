using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Dapper.Repositories;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Mysql.Repositories
{
    /// <summary>
    ///     A mysql language repository.
    /// </summary>
    public class MysqlLanguageRepository : DapperLanguageRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public MysqlLanguageRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>
        ///     Gets the two letter isoes in this collection.
        /// </summary>
        /// <param name="cultureTwoLetterIsoLanguageName">  Name of the culture two letter ISO language. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the two letter isoes in this
        ///     collection.
        /// </returns>
        public override IEnumerable<LanguageEntity> GetByTwoLetterIso(string cultureTwoLetterIsoLanguageName)
        {
            var command = GetFromCache(() =>
                    $"SELECT * FROM {SqlBuilder.Adapter.RenderTableName(typeof(LanguageEntity))} " +
                    $"WHERE {SqlBuilder.Adapter.RenderPropertyName(nameof(LanguageEntity.IsoName))} " +
                    $"LIKE CONCAT({SqlBuilder.Adapter.RenderParameterProperty(nameof(cultureTwoLetterIsoLanguageName))}, '%')",
                nameof(GetByTwoLetterIso), nameof(cultureTwoLetterIsoLanguageName));

            return UnitOfWork.Connection.Query<LanguageEntity>(command, new {cultureTwoLetterIsoLanguageName},
                UnitOfWork.Transaction);
        }

        /// <summary>
        ///     Gets by two letter ISO asynchronous.
        /// </summary>
        /// <param name="cultureTwoLetterIsoLanguageName">  Name of the culture two letter ISO language. </param>
        /// <returns>
        ///     The by two letter ISO.
        /// </returns>
        public override Task<IEnumerable<LanguageEntity>> GetByTwoLetterIsoAsync(string cultureTwoLetterIsoLanguageName)
        {
            var command = GetFromCache(() =>
                    $"SELECT * FROM {SqlBuilder.Adapter.RenderTableName(typeof(LanguageEntity))} " +
                    $"WHERE {SqlBuilder.Adapter.RenderPropertyName(nameof(LanguageEntity.IsoName))} " +
                    $"LIKE CONCAT({SqlBuilder.Adapter.RenderParameterProperty(nameof(cultureTwoLetterIsoLanguageName))}, '%')",
                nameof(GetByTwoLetterIso), nameof(cultureTwoLetterIsoLanguageName));

            return UnitOfWork.Connection.QueryAsync<LanguageEntity>(command, new {cultureTwoLetterIsoLanguageName},
                UnitOfWork.Transaction);
        }
    }
}