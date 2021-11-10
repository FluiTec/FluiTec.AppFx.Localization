using System.IO;
using FluiTec.AppFx.Data.Dapper;
using FluiTec.AppFx.Localization.TestLibrary.Configuration;
using FluiTec.AppFx.Options.Helpers;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Configuration;
using ConfigurationManager = FluiTec.AppFx.Options.Managers.ConfigurationManager;

namespace FluiTec.AppFx.Localization.TestLibrary
{
    /// <summary>
    ///     A dapper localization data service provider.
    /// </summary>
    public abstract class DapperLocalizationDataServiceProvider : LocalizationDataServiceProvider
    {
        /// <summary>
        ///     Specialized default constructor for use only by derived class.
        /// </summary>
        protected DapperLocalizationDataServiceProvider()
        {
            var path = DirectoryHelper.GetApplicationRoot();
            var parent = Directory.GetParent(path)?.Parent?.Parent?.Parent?.FullName;
            var config = new ConfigurationBuilder()
                .SetBasePath(parent)
                .AddJsonFile("appsettings.integration.json", false, true)
                .AddJsonFile("appsettings.integration.secret.json", true, true)
                .Build();
            ConfigurationManager = new ConfigurationManager(config);

            // ReSharper disable VirtualMemberCallInConstructor
            ServiceOptions = ConfigureOptions();
            AdminOptions = ConfigureAdminOptions();
            // ReSharper enable VirtualMemberCallInConstructor
        }

        /// <summary>
        ///     Gets or sets options for controlling the service.
        /// </summary>
        /// <value>
        ///     Options that control the service.
        /// </value>
        public IDapperServiceOptions ServiceOptions { get; protected set; }

        /// <summary>
        ///     Gets or sets options for controlling the admin.
        /// </summary>
        /// <value>
        ///     Options that control the admin.
        /// </value>
        public DbAdminOptions AdminOptions { get; protected set; }

        /// <summary>
        ///     Gets the manager for configuration.
        /// </summary>
        /// <value>
        ///     The configuration manager.
        /// </value>
        protected ConfigurationManager ConfigurationManager { get; }

        /// <summary>
        ///     Gets a value indicating whether the database is available.
        /// </summary>
        /// <value>
        ///     True if the database is available, false if not.
        /// </value>
        public override bool IsDbAvailable => ServiceOptions != null;

        /// <summary>
        ///     Configure options.
        /// </summary>
        /// <returns>
        ///     The IDapperServiceOptions.
        /// </returns>
        protected abstract IDapperServiceOptions ConfigureOptions();

        /// <summary>
        ///     Configure admin options.
        /// </summary>
        /// <returns>
        ///     The DbAdminOptions.
        /// </returns>
        protected abstract DbAdminOptions ConfigureAdminOptions();
    }
}