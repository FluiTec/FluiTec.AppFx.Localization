using FluiTec.AppFx.Localization;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Options.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using WebSample.Models;

namespace WebSample
{
    /// <summary>   A startup. </summary>
    public class Startup
    {
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

        #region Properties

        /// <summary>	Gets the configuration. </summary>
        /// <value>	The configuration. </value>
        public IConfigurationRoot Configuration { get; }

        /// <summary>Gets the environment.</summary>
        /// <value>The environment.</value>
        public IWebHostEnvironment Environment { get; }

        #endregion

        #region Methods

        /// <summary>   Configure services. </summary>
        /// <param name="services"> The services. </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var configManager = new ConsoleReportingConfigurationManager(Configuration);

            services
                .ConfigureDynamicLocalizationDataProvider(configManager)
                .ConfigureLocalization(configManager);
                
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        /// <summary>   Configures. </summary>
        /// <param name="app">  The application. </param>
        /// <param name="env">  The environment. </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var fact = app.ApplicationServices.GetRequiredService<IStringLocalizerFactory>();
            var localizer = fact.Create(typeof(TestModel));
            var strings = localizer.GetAllStrings();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();
            app.UseDbLocalizationProvider(app.ApplicationServices.GetRequiredService<CultureOptions>(), new RequestLocalizationOptions());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }

        #endregion
    }
}