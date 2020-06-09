using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>Interface for resource repository.</summary>
    public interface IResourceRepository : IWritableKeyTableDataRepository<ResourceEntity, int>
    {
        /// <summary>   Gets by key. </summary>
        /// <param name="key">  The key. </param>
        /// <returns>   The by key. </returns>
        ResourceEntity GetByKey(string key);

        /// <summary>   Resets the synchronise status. </summary>
        void ResetSyncStatus();

        /// <summary>Refactor key.</summary>
        /// <param name="oldKey">   The old key. </param>
        /// <param name="newKey">   The new key. </param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        bool RefactorKey(string oldKey, string newKey);
    }
}