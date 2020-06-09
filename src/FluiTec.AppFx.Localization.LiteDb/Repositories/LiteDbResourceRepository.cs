using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb.Repositories
{
    /// <summary>   A lite database resource repository. </summary>
    public class LiteDbResourceRepository : LiteDbWritableIntegerKeyTableDataRepository<ResourceEntity>, IResourceRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbResourceRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public ResourceEntity GetByKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public void ResetSyncStatus()
        {
            throw new System.NotImplementedException();
        }

        public bool RefactorKey(string oldKey, string newKey)
        {
            throw new System.NotImplementedException();
        }
    }
}