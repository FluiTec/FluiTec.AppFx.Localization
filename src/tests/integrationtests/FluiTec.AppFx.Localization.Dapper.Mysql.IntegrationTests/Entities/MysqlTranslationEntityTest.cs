using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mysql.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql translation entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class MysqlTranslationEntityTest : TranslationEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public MysqlTranslationEntityTest() : base(new MysqlLocalizationDataServiceProvider())
        {
        }
    }
}