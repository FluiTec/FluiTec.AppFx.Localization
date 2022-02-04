using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mysql.IntegrationTests.Entities;

/// <summary>
///     (Unit Test Class) a mssql language entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class MysqlLanguageEntityTest : LanguageEntityTest
{
    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    public MysqlLanguageEntityTest() : base(new MysqlLocalizationDataServiceProvider())
    {
    }
}