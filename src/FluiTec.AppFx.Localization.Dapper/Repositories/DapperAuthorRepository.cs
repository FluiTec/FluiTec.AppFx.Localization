using System.Threading.Tasks;
using Dapper;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Repositories
{
    /// <summary>
    ///     A dapper author repository.
    /// </summary>
    public class DapperAuthorRepository : DapperWritableKeyTableDataRepository<AuthorEntity, int>, IAuthorRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public DapperAuthorRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>
        ///     Gets an author entity using the given name.
        /// </summary>
        /// <param name="name"> The name to get. </param>
        /// <returns>
        ///     An AuthorEntity.
        /// </returns>
        public AuthorEntity Get(string name)
        {
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(AuthorEntity), nameof(AuthorEntity.Name)),
                nameof(Get), nameof(name));

            return UnitOfWork.Connection.QuerySingle<AuthorEntity>(command, new {Name = name}, UnitOfWork.Transaction);
        }

        /// <summary>
        ///     Gets an asynchronous.
        /// </summary>
        /// <param name="name"> The name to get. </param>
        /// <returns>
        ///     The asynchronous.
        /// </returns>
        public Task<AuthorEntity> GetAsync(string name)
        {
            var command = GetFromCache(() =>
                    SqlBuilder.SelectByFilter(typeof(AuthorEntity), nameof(AuthorEntity.Name)),
                nameof(Get), nameof(name));

            return UnitOfWork.Connection.QuerySingleAsync<AuthorEntity>(command, new {Name = name},
                UnitOfWork.Transaction);
        }
    }
}