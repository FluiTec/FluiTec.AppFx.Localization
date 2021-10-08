using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Sqlite.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql translation entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class SqliteTranslationEntityTest : TranslationEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public SqliteTranslationEntityTest() : base(new SqliteLocalizationDataServiceProvider())
        {
        }
    }
}