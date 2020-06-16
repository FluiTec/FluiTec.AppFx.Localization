using System.Collections.Generic;
using Dapper;
using FluiTec.AppFx.Data.Dapper.Repositories;
using FluiTec.AppFx.Data.Dapper.UnitsOfWork;
using FluiTec.AppFx.Data.Repositories;
using FluiTec.AppFx.Localization.Compound;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Repositories;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Dapper.Repositories
{
    /// <summary>A translation repository.</summary>
    public abstract class TranslationRepository : DapperWritableKeyTableDataRepository<TranslationEntity, int>,
        ITranslationRepository
    {
        /// <summary>   Specialized constructor for use only by derived class. </summary>
        /// <param name="unitOfWork">   The unit of work. </param>
        /// <param name="logger">       The logger. </param>
        protected TranslationRepository(DapperUnitOfWork unitOfWork, ILogger<IRepository> logger) : base(unitOfWork,
            logger)
        {
        }

        /// <summary>   Enumerates by resource in this collection. </summary>
        /// <param name="resource"> The resource. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process by resource in this collection.
        /// </returns>
        public IEnumerable<TranslationEntity> ByResource(ResourceEntity resource)
        {
            var command = SqlBuilder.SelectByFilter(EntityType, nameof(TranslationEntity.ResourceId));
            return UnitOfWork.Connection.Query<TranslationEntity>(command, new {ResourceId = resource.Id},
                UnitOfWork.Transaction);
        }

        /// <summary>   Gets all compounds in this collection. </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process all compounds in this collection.
        /// </returns>
        public virtual IEnumerable<CompoundTranslationEntity> GetAllCompound()
        {
            var command = CreateGetAllCompoundQueryCommand();
            var lookup = new Dictionary<int, CompoundTranslationEntity>();
            UnitOfWork.Connection
                .Query<ResourceEntity, TranslationEntity, CompoundTranslationEntity>(command,
                    (resource, translation) =>
                    {
                        // make sure the pk exists
                        if (resource == null || resource.Id == default)
                            return null;

                        // make sure our list contains the pk
                        if (!lookup.ContainsKey(resource.Id))
                            lookup.Add(resource.Id, new CompoundTranslationEntity
                            {
                                Resource = resource
                            });

                        // fetch the real element
                        var tempElem = lookup[resource.Id];

                        // add translations
                        if (translation != null)
                            tempElem.Translations.Add(translation);

                        return tempElem;
                    }, null, UnitOfWork.Transaction);
            return lookup.Values;
        }

        /// <summary>   Creates get all compound query command. </summary>
        /// <returns>   The new get all compound query command. </returns>
        protected abstract string CreateGetAllCompoundQueryCommand();
    }
}