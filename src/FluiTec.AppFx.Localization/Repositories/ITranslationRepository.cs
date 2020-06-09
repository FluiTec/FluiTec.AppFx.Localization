using System.Collections.Generic;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Compound;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>Interface for translation repository.</summary>
    public interface ITranslationRepository : IWritableKeyTableDataRepository<TranslationEntity, int>
    {
        /// <summary>   Enumerates by resource in this collection. </summary>
        /// <param name="resource"> The resource. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process by resource in this collection.
        /// </returns>
        IEnumerable<TranslationEntity> ByResource(ResourceEntity resource);

        /// <summary>   Gets all compounds in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all compounds in this collection.
        /// </returns>
        IEnumerable<CompoundTranslationEntity> GetAllCompound();
    }
}