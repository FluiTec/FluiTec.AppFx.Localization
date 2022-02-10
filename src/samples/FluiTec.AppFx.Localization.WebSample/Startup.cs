using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Dynamic.Extensions;
using FluiTec.AppFx.Localization.Import;
using FluiTec.AppFx.Options.Managers;

namespace FluiTec.AppFx.Localization.WebSample;

/// <summary>
/// A startup.
/// </summary>
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

    /// <summary>
    /// Constructor.
    /// </summary>
    ///
    /// <param name="environment">  The environment. </param>
    public Startup(IWebHostEnvironment environment)
    {
        Environment = environment;

        var builder = new ConfigurationBuilder()
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("appsettings.secret.json", false, true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true);

        builder.AddEnvironmentVariables();
        Configuration = builder.Build();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Configure services.
    /// </summary>
    ///
    /// <param name="services"> The services. </param>
    public void ConfigureServices(IServiceCollection services)
    {
        var manager = new ConsoleReportingConfigurationManager(Configuration);

        // IMPORTANT
        services
            .AddControllersWithViews()
            .AddDataAnnotationsLocalization();
        // IMPORTANT

        services.ConfigureLocalization(manager);
    }

    /// <summary>
    /// Configure application.
    /// </summary>
    ///
    /// <param name="app">              The application. </param>
    /// <param name="loggerFactory">    The logger factory. </param>
    /// <param name="appLifetime">      The application lifetime. </param>
    /// <param name="serviceProvider">  The service provider. </param>
    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostApplicationLifetime appLifetime,
        IServiceProvider serviceProvider)
    {
        var changes = serviceProvider.GetRequiredService<ILocalizationImportService>().Import();
        var res = serviceProvider.GetRequiredService<ILocalizationDataService>().BeginUnitOfWork().ResourceRepository.GetAll();
        
        // Configure the HTTP request pipeline.
        if (!Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        // IMPORTANT
        app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<ServiceLocalizationOptions>().GetRequestLocalizationOptions());
        // IMPORTANT

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });
    }

    #endregion
}