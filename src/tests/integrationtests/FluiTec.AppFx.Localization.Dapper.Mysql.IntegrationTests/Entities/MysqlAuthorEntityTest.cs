using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mysql.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql author entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlAuthorEntityTest : AuthorEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public MysqlAuthorEntityTest() : base(new MysqlLocalizationDataServiceProvider())
        {
        }
    }
}