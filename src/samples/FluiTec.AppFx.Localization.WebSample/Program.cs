using System.Globalization;

namespace FluiTec.AppFx.Localization.WebSample;

public class Program
{
    public static void Main(string[] args)
    {
        CultureInfo.CurrentCulture = new CultureInfo("de");
        CultureInfo.CurrentUICulture = new CultureInfo("de");

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}