using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using FluiTec.AppFx.Data.Dapper.DataServices;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Localization.TestLibrary.Configuration;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.IntegrationTests
{
    /// <summary>
    /// (Unit Test Class) an initialize.
    /// </summary>
    [TestClass]
    public static class Initialize
    {
        /// <summary>
        /// Initializes this object.
        /// </summary>
        ///
        /// <param name="context">  The context. </param>
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var pw = Environment.GetEnvironmentVariable("SA_PASSWORD");

            MssqlDapperServiceOptions serviceOptions = null;
            MssqlLocalizationDataService dataService = null;

            if (!string.IsNullOrWhiteSpace(pw))
            {
                serviceOptions = new MssqlDapperServiceOptions
                {
                    ConnectionString =
                        $"Data Source=mssql;Initial Catalog=master;Integrated Security=False;User ID=sa;Password={pw};Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
                };

                dataService = new MssqlLocalizationDataService(serviceOptions, null);
            }
            else
            {
                try
                {
                    var path = DirectoryHelper.GetApplicationRoot();
                    var parent = Directory.GetParent(path)?.Parent?.Parent?.FullName;
                    var config = new ConfigurationBuilder()
                        .SetBasePath(parent)
                        .AddJsonFile("appsettings.integration.json", false, true)
                        .AddJsonFile("appsettings.integration.secret.json", true, true)
                        .Build();

                    var manager = new ConfigurationManager(config);
                    var options = manager.ExtractSettings<MssqlAdminOption>();
                    var mssqlOptions = manager.ExtractSettings<MssqlDapperServiceOptions>();

                    if (string.IsNullOrWhiteSpace(options.AdminConnectionString) ||
                        string.IsNullOrWhiteSpace(options.IntegrationDb) ||
                        string.IsNullOrWhiteSpace(options.IntegrationUser) ||
                        string.IsNullOrWhiteSpace(options.IntegrationPassword)) return;
                    if (string.IsNullOrWhiteSpace(mssqlOptions.ConnectionString)) return;

                    MssqlAdminHelper.CreateDababase(options.AdminConnectionString, options.IntegrationDb);
                    MssqlAdminHelper.CreateUserAndLogin(options.AdminConnectionString, options.IntegrationDb,
                        options.IntegrationUser, options.IntegrationPassword);

                    serviceOptions = new MssqlDapperServiceOptions
                    {
                        ConnectionString = mssqlOptions.ConnectionString
                    };
                    dataService = new MssqlLocalizationDataService(serviceOptions, null);
                }
                catch (Exception)
                {
                    // ignore
                }
            }

            if (serviceOptions == null || dataService == null) return;

            var migrator = new DapperDataMigrator(serviceOptions.ConnectionString,
                new[] {dataService.GetType().Assembly}, ((IDapperDataService) dataService).MetaData,
                builder => builder.AddSqlServer());
            migrator.Migrate();
        }
    }
}
