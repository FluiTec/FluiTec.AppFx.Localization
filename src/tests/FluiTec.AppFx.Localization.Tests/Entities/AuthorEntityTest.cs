using FluiTec.AppFx.Localization.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Tests.Entities
{
    /// <summary>
    /// (Unit Test Class) an author entity test.
    /// </summary>
    [TestClass]
    public class AuthorEntityTest : EntityTest<AuthorEntity>
    {
        /// <summary>
        /// Creates the entity.
        /// </summary>
        ///
        /// <returns>
        /// The new entity.
        /// </returns>
        protected override AuthorEntity CreateEntity()
        {
            return new AuthorEntity {Id = 1, Name = "Max Mustermann"};
        }

        /// <summary>
        /// Creates other entity.
        /// </summary>
        ///
        /// <returns>
        /// The new other entity.
        /// </returns>
        protected override AuthorEntity CreateOtherEntity()
        {
            return new AuthorEntity {Id = 2, Name = "John Doe"};
        }
    }
}