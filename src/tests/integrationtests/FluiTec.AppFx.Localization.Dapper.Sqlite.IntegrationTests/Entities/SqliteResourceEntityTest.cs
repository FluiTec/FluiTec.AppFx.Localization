using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Sqlite.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql resource entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class SqliteResourceEntityTest : ResourceEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public SqliteResourceEntityTest() : base(new SqliteLocalizationDataServiceProvider())
        {
        }
    }
}