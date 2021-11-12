using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Pgsql.IntegrationTests.Entities;

/// <summary>
///     (Unit Test Class) a mssql language entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class PgsqlLanguageEntityTest : LanguageEntityTest
{
    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    public PgsqlLanguageEntityTest() : base(new PgsqlLocalizationDataServiceProvider())
    {
    }
}