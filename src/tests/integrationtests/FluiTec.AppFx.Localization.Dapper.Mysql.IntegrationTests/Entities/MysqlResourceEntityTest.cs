using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mysql.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql resource entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlResourceEntityTest : ResourceEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public MysqlResourceEntityTest() : base(new MysqlLocalizationDataServiceProvider())
        {
        }
    }
}