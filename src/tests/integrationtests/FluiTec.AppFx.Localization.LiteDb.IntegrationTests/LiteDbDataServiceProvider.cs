using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;

namespace FluiTec.AppFx.Localization.LiteDb.IntegrationTests;

/// <summary>
///     A lite database data service provider.
/// </summary>
public class LiteDbDataServiceProvider : LiteDbDataServiceProvider<ILocalizationDataService, ILocalizationUnitOfWork>
{
    /// <summary>
    ///     Gets a value indicating whether the database is available.
    /// </summary>
    /// <value>
    ///     True if the database is available, false if not.
    /// </value>
    public override bool IsDbAvailable => true;

    /// <summary>
    ///     Provide data service.
    /// </summary>
    /// <returns>
    ///     A TDataService.
    /// </returns>
    public override ILocalizationDataService ProvideDataService()
    {
        return new LiteDbLocalizationDataService(ConfigureOptions(), null);
    }
}