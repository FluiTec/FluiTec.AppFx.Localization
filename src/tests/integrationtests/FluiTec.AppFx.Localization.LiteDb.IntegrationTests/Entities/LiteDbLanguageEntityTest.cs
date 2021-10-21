using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.LiteDb.IntegrationTests.Entities
{
    /// <summary>
    /// (Unit Test Class) a lite database language entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class LiteDbLanguageEntityTest : LanguageEntityTest
    {
        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        protected LiteDbLanguageEntityTest() : base(new LiteDbDataServiceProvider())
        {
        }
    }
}