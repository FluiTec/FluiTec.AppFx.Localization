using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.LiteDb.IntegrationTests.Entities;

/// <summary>
///     (Unit Test Class) a lite database resource entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class LiteDbResourceEntityTest : ResourceEntityTest
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public LiteDbResourceEntityTest() : base(new LiteDbDataServiceProvider())
    {
    }
}