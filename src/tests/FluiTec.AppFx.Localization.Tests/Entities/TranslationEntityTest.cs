using FluiTec.AppFx.Localization.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Tests.Entities
{
    /// <summary>
    /// (Unit Test Class) a translation entity test.
    /// </summary>
    [TestClass]
    public class TranslationEntityTest : EntityTest<TranslationEntity>
    {
        /// <summary>
        /// Creates the entity.
        /// </summary>
        ///
        /// <returns>
        /// The new entity.
        /// </returns>
        protected override TranslationEntity CreateEntity()
        {
            return new TranslationEntity
            {
                Id = 1,
                LanguageId = 1,
                ResourceId = 1,
                Value = "Translation1"
            };
        }

        /// <summary>
        /// Creates other entity.
        /// </summary>
        ///
        /// <returns>
        /// The new other entity.
        /// </returns>
        protected override TranslationEntity CreateOtherEntity()
        {
            return new TranslationEntity
            {
                Id = 2,
                LanguageId = 2,
                ResourceId = 2,
                Value = "Translation2"
            };
        }
    }
}