using System.Collections.Generic;
using FluiTec.AppFx.Data.LiteDb.Repositories;
using FluiTec.AppFx.Data.LiteDb.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Compound;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.LiteDb.Repositories
{
    /// <summary>   A lite database translation repository. </summary>
    public class LiteDbTranslationRepository : LiteDbWritableIntegerKeyTableDataRepository<TranslationEntity>, ITranslationRepository
    {
        /// <summary>   Constructor. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        public LiteDbTranslationRepository(LiteDbUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork, logger)
        {
        }

        public IEnumerable<TranslationEntity> ByResource(ResourceEntity resource)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CompoundTranslationEntity> GetAllCompound()
        {
            throw new System.NotImplementedException();
        }
    }
}