using System;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dapper.Mysql;
using FluiTec.AppFx.Data.Dapper.Pgsql;
using FluiTec.AppFx.Data.Dapper.SqLite;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Localization;
using FluiTec.AppFx.Localization.Dapper.Mssql;
using FluiTec.AppFx.Localization.Dapper.Mysql;
using FluiTec.AppFx.Localization.Dapper.Pgsql;
using FluiTec.AppFx.Localization.Dapper.Sqlite;
using FluiTec.AppFx.Localization.LiteDb;
using FluiTec.AppFx.Options.Managers;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>   A dynamic localization data configuration extension. </summary>
    public static class DynamicLocalizationDataConfigurationExtension
    {
        /// <summary>
        ///     An IServiceCollection extension method that configure dynamic localization data
        ///     provider.
        /// </summary>
        /// <param name="services">             The services to act on. </param>
        /// <param name="configurationManager"> Manager for configuration. </param>
        /// <returns>   An IServiceCollection. </returns>
        public static IServiceCollection ConfigureDynamicLocalizationDataProvider(this IServiceCollection services,
            ConfigurationManager configurationManager)
        {
            services.ConfigureDynamicDataProvider(configurationManager,
                new Func<DynamicDataOptions, IServiceProvider, ILocalizationDataService>((options, provider) =>
                    {
                        return options.Provider switch
                        {
                            DataProvider.LiteDb => new LiteDbLocalizationDataService(
                                provider.GetRequiredService<LiteDbServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Mssql => new MssqlLocalizationDataService(
                                provider.GetRequiredService<MssqlDapperServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Mysql => new MysqlLocalizationDataService(
                                provider.GetRequiredService<MysqlDapperServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Pgsql => new PgsqlLocalizationDataService(
                                provider.GetRequiredService<PgsqlDapperServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            DataProvider.Sqlite => new SqliteLocalizationDataService(
                                provider.GetRequiredService<SqliteDapperServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            _ => throw new NotImplementedException()
                        };
                    }
                )
            );

            services.AddDbLocalizationProvider(configurationManager);

            return services;
        }
    }
}