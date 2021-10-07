using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
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

            MssqlAdminHelper.CreateDababase(provider.AdminOptions.AdminConnectionString ?? provider.ServiceOptions.ConnectionString,
                provider.AdminOptions.IntegrationDb);
            MssqlAdminHelper.CreateUserAndLogin(provider.AdminOptions.AdminConnectionString,
                provider.AdminOptions.IntegrationDb,
                provider.AdminOptions.IntegrationUser, provider.AdminOptions.IntegrationPassword);

            var migrator = new DapperDataMigrator(provider.ServiceOptions.ConnectionString,
                new[] {dataService.GetType().BaseType?.Assembly}, ((IDapperDataService) dataService).MetaData,
                builder => builder.AddSqlServer());
            migrator.Migrate();
        }
    }
}