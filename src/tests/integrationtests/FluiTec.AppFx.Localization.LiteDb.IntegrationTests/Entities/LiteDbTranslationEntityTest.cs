using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.LiteDb.IntegrationTests.Entities
{
    /// <summary>
    /// (Unit Test Class) a lite database translation entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class LiteDbTranslationEntityTest : TranslationEntityTest
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LiteDbTranslationEntityTest()  : base(new LiteDbDataServiceProvider())
        {
        }
    }
}