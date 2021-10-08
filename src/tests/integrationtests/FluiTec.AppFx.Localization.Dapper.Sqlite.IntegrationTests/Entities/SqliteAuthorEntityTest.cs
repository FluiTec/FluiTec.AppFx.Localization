using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Sqlite.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql author entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class SqliteAuthorEntityTest : AuthorEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public SqliteAuthorEntityTest() : base(new SqliteLocalizationDataServiceProvider())
        {
        }
    }
}