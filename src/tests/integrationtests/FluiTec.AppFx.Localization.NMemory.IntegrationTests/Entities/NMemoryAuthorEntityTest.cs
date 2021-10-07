using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.NMemory.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a memory author entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class NMemoryAuthorEntityTest : AuthorEntityTest
    {
        /// <summary>
        ///     Default constructor.
        /// </summary>
        public NMemoryAuthorEntityTest() : base(new NMemoryDataServiceProvider())
        {
        }
    }
}