using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.LiteDb.IntegrationTests.Entities;

/// <summary>
///     (Unit Test Class) a lite database author entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class LiteDbAuthorEntityTest : AuthorEntityTest
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public LiteDbAuthorEntityTest() : base(new LiteDbDataServiceProvider())
    {
    }
}