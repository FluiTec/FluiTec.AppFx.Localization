using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Sqlite.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class Initialize
    {
        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var provider = new SqliteLocalizationDataServiceProvider();
            var dataService = provider.ProvideDataService();

            if (System.IO.File.Exists("mydb.db"))
                System.IO.File.Delete("mydb.db");

            dataService.GetMigrator().Migrate();
        }
    }
}