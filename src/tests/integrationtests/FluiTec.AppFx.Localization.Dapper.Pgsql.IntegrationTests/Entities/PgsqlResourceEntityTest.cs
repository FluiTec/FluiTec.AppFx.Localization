using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Pgsql.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql resource entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlResourceEntityTest : ResourceEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public PgsqlResourceEntityTest() : base(new PgsqlLocalizationDataServiceProvider())
        {
        }
    }
}