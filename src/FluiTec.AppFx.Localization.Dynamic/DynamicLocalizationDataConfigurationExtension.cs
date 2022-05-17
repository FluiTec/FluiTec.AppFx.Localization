using System;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Localization.Dapper.Mssql;
using FluiTec.AppFx.Localization.Dapper.Mysql;
using FluiTec.AppFx.Localization.Dapper.Pgsql;
using FluiTec.AppFx.Localization.LiteDb;
using FluiTec.AppFx.Localization.NMemory;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FluiTec.AppFx.Localization.Dynamic
{
    /// <summary>
    ///     A dynamic localization data configuration extension.
    /// </summary>
    public static class DynamicLocalizationDataConfigurationExtension
    {
        /// <summary>
        ///     An IServiceCollection extension method that configure dynamic localization data provider.
        /// </summary>
        /// <param name="services">             The services to act on. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <returns>
        ///     An IServiceCollection.
        /// </returns>
        public static IServiceCollection ConfigureDynamicLocalizationDataProvider(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.ConfigureDynamicDataProvider<ILocalizationDataService, LocalizationDynamicDataOptions>(
                configurationManager,
                (options, provider) =>
                {
                    return options.CurrentValue.Provider switch
                    {
                        DataProvider.LiteDb => new LiteDbLocalizationDataService(
                            provider.GetRequiredService<IOptionsMonitor<LiteDbServiceOptions>>(),
                            provider.GetService<ILoggerFactory>()),
                        DataProvider.Mssql => new MssqlLocalizationDataService(
                            provider.GetRequiredService<IOptionsMonitor<MssqlDapperServiceOptions>>(),
                            provider.GetService<ILoggerFactory>()),
                        DataProvider.Pgsql => new PgsqlLocalizationDataService(
                            provider.GetRequiredService<IOptionsMonitor<PgsqlDapperServiceOptions>>(),
                            provider.GetService<ILoggerFactory>()),
                        DataProvider.Mysql => new MysqlLocalizationDataService(
                            provider.GetRequiredService<IOptionsMonitor<MysqlDapperServiceOptions>>(),
                            provider.GetService<ILoggerFactory>()),
                        DataProvider.NMemory => new NMemoryLocalizationDataService(
                            provider.GetService<ILoggerFactory>()),
                        _ => throw new NotImplementedException()
                    };
                }
            );

            return services;
        }
    }
}