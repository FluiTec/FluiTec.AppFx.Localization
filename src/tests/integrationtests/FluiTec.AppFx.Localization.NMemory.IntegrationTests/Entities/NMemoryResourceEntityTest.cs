using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.NMemory.IntegrationTests.Entities
{
    /// <summary>
    ///     A memory resource entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class NMemoryResourceEntityTest : ResourceEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public NMemoryResourceEntityTest() : base(new NMemoryDataServiceProvider())
        {
        }
    }
}