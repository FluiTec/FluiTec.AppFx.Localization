using FluiTec.AppFx.Localization.TestLibrary.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.NMemory.IntegrationTests.Entities;

/// <summary>
///     A memory translation entity test.
/// </summary>
[TestClass]
[TestCategory("Integration")]
public class NMemoryTranslationEntityTest : TranslationEntityTest
{
    /// <summary>
    ///     Default constructor.
    /// </summary>
    public NMemoryTranslationEntityTest() : base(new NMemoryDataServiceProvider())
    {
    }
}