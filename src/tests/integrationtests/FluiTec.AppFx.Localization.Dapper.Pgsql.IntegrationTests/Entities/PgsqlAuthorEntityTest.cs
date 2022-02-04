using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Pgsql.IntegrationTests.Entities;

/// <summary>
///     (Unit Test Class) a mssql author entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class PgsqlAuthorEntityTest : AuthorEntityTest
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public PgsqlAuthorEntityTest() : base(new PgsqlLocalizationDataServiceProvider())
    {
    }
}