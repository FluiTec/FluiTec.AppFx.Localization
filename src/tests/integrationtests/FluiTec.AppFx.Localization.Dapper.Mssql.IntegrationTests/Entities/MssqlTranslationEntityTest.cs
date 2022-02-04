using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.IntegrationTests.Entities;

/// <summary>
///     (Unit Test Class) a mssql translation entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class MssqlTranslationEntityTest : TranslationEntityTest
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public MssqlTranslationEntityTest() : base(new MssqlLocalizationDataServiceProvider())
    {
    }
}