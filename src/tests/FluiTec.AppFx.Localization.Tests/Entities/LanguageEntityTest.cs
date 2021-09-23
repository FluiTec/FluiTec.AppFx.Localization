using FluiTec.AppFx.Localization.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Tests.Entities
{
    /// <summary>
    /// (Unit Test Class) a language entity test.
    /// </summary>
    [TestClass]
    public class LanguageEntityTest : EntityTest<LanguageEntity>
    {
        /// <summary>
        /// Creates the entity.
        /// </summary>
        ///
        /// <returns>
        /// The new entity.
        /// </returns>
        protected override LanguageEntity CreateEntity()
        {
            return new LanguageEntity {Id = 1, Name = "de-DE"};
        }

        /// <summary>
        /// Creates other entity.
        /// </summary>
        ///
        /// <returns>
        /// The new other entity.
        /// </returns>
        protected override LanguageEntity CreateOtherEntity()
        {
            return new LanguageEntity {Id = 2, Name = "en-US"};
        }
    }
}