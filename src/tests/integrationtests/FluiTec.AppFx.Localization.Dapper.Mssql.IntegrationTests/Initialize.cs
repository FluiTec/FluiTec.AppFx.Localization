using FluiTec.AppFx.Data.Dapper.Mssql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.IntegrationTests
{
    /// <summary>   An initialize.</summary>
    [TestClass]
    public static class Initialize
    {
        /// <summary>   Initializes this Initialize.</summary>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var provider = new MssqlLocalizationDataServiceProvider();
            var dataService = provider.ProvideDataService();

            System.Console.WriteLine("###################################");
            System.Console.WriteLine("###################################");
            System.Console.WriteLine("###################################");
            System.Console.WriteLine(provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString);
            System.Console.WriteLine("###################################");
            System.Console.WriteLine("###################################");
            System.Console.WriteLine("###################################");
            
            MssqlAdminHelper.CreateDababase(provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString,
                provider.AdminOptions.IntegrationDb);
            MssqlAdminHelper.CreateUserAndLogin(provider.AdminOptions.AdminConnectionString,
                provider.AdminOptions.IntegrationDb,
                provider.AdminOptions.IntegrationUser, provider.AdminOptions.IntegrationPassword);

            dataService.GetMigrator().Migrate();
        }
    }
}