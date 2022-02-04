using System.IO;
using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.LiteDb.IntegrationTests;

/// <summary>   An initialize.</summary>
[TestClass]
public static class Initialize
{
    /// <summary>   Initializes this Initialize.</summary>
    [AssemblyInitialize]
    public static void Init(TestContext context)
    {
        var provider =
            new LiteDbDataServiceProvider() as
                LiteDbDataServiceProvider<ILocalizationDataService, ILocalizationUnitOfWork>;
        var options = provider.ConfigureOptions();
        if (File.Exists(options.FullDbFilePath))
            File.Delete(options.FullDbFilePath);
    }
}