using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql resource entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MssqlResourceEntityTest : ResourceEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public MssqlResourceEntityTest() : base(new MssqlLocalizationDataServiceProvider())
        {
        }
    }
}