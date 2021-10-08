using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Pgsql.IntegrationTests.Entities
{
    /// <summary>
    ///     (Unit Test Class) a mssql translation entity test.
    /// </summary>
    [TestClass]
    [TestCategory("Integration")]
    public class PgsqlTranslationEntityTest : TranslationEntityTest
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        public PgsqlTranslationEntityTest() : base(new PgsqlLocalizationDataServiceProvider())
        {
        }
    }
}