using System;
using System.Security.Policy;
using FluiTec.AppFx.Data.Dapper.Mssql;
using FluiTec.AppFx.Data.Dynamic.Configuration;
using FluiTec.AppFx.Data.LiteDb;
using FluiTec.AppFx.Localization;
using FluiTec.AppFx.Localization.Dapper.Mssql;
using FluiTec.AppFx.Options.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebSample
{
    /// <summary>   A startup. </summary>
    public class Startup
    {
        #region Properties

        /// <summary>	Gets the configuration. </summary>
        /// <value>	The configuration. </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>Gets the environment.</summary>
        /// <value>The environment.</value>
        public IWebHostEnvironment Environment { get; }

        #endregion

        #region Constructors

        /// <summary>   Constructor. </summary>
        /// <param name="environment">  The environment. </param>
        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;

            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        #endregion

        #region Methods

        /// <summary>   Configure services. </summary>
        /// <param name="services"> The services. </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var configManager = new ConsoleReportingConfigurationManager(Configuration);

            services.ConfigureDynamicDataProvider(configManager,
                new Func<DynamicDataOptions, IServiceProvider, ILocalizationDataService>((options, provider) =>
                    {
                        return options.Provider switch
                        {
                            DataProvider.Mssql => new MssqlLocalizationDataService(
                                provider.GetRequiredService<MssqlDapperServiceOptions>(),
                                provider.GetService<ILoggerFactory>()),
                            _ => throw new NotImplementedException()
                        };
                    }
                )
            );

            services.ConfigureDynamicLocalizationDataProvider(configManager);
        }

        /// <summary>   Configures. </summary>
        /// <param name="app">  The application. </param>
        /// <param name="env">  The environment. </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            // this line finds all resources and models in code and config-files
            // adds/updates them in the database - and initializes a cache
            app.UseDbLocalizationProvider();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var service = app.ApplicationServices.CreateScope().ServiceProvider
                        .GetRequiredService<ILocalizationDataService>();
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        #endregion
    }
}
