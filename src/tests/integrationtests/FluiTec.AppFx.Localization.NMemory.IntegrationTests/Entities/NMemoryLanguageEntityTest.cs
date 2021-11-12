using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.NMemory.IntegrationTests.Entities;

/// <summary>
///     (Unit Test Class) a memory language entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class NMemoryLanguageEntityTest : LanguageEntityTest
{
    /// <summary>
    ///     Specialized constructor for use only by derived class.
    /// </summary>
    public NMemoryLanguageEntityTest() : base(new NMemoryDataServiceProvider())
    {
    }
}