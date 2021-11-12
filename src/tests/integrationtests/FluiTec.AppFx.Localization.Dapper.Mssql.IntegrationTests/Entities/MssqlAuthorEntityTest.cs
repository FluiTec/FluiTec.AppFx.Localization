using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.IntegrationTests.Entities;

/// <summary>
///     (Unit Test Class) a mssql author entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class MssqlAuthorEntityTest : AuthorEntityTest
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public MssqlAuthorEntityTest() : base(new MssqlLocalizationDataServiceProvider())
    {
    }
}