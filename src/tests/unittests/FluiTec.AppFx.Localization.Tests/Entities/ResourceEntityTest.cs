using System;
using FluiTec.AppFx.Localization.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Tests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a resource entity test.
    /// </summary>
    [TestClass]
    public class ResourceEntityTest : EntityTest<ResourceEntity>
    {
        /// <summary>
        ///     Creates the entity.
        /// </summary>
        /// <returns>
        ///     The new entity.
        /// </returns>
        protected override ResourceEntity CreateEntity()
        {
            return new ResourceEntity
            {
                Id = 1,
                AuthorId = 1,
                ModificationDate = new DateTime(2021, 09, 22),
                Key = "Key1"
            };
        }

        /// <summary>
        ///     Creates other entity.
        /// </summary>
        /// <returns>
        ///     The new other entity.
        /// </returns>
        protected override ResourceEntity CreateOtherEntity()
        {
            return new ResourceEntity
            {
                Id = 2,
                AuthorId = 2,
                ModificationDate = new DateTime(2021, 09, 21),
                Key = "Key2"
            };
        }
    }
}