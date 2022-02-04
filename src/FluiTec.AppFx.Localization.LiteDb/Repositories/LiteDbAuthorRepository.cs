using System.Threading.Tasks;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb.Repositories
{
    /// <summary>
    ///     A lite database author repository.
    /// </summary>
    public class LiteDbAuthorRepository : LiteDbWritableIntegerKeyTableDataRepository<AuthorEntity>, IAuthorRepository
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbAuthorRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
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
            return Collection.FindOne(entity => entity.Name == name);
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
            return Task.FromResult(Get(name));
        }
    }
}