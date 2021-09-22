using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Repositories
{
    /// <summary>
    /// Interface for author repository.
    /// </summary>
    public interface IAuthorRepository : IWritableKeyTableDataRepository<AuthorEntity, int>
    {
        /// <summary>
        /// Gets an author entity using the given name.
        /// </summary>
        ///
        /// <param name="name"> The name to get. </param>
        ///
        /// <returns>
        /// An AuthorEntity.
        /// </returns>
        AuthorEntity Get(string name);
    }
}