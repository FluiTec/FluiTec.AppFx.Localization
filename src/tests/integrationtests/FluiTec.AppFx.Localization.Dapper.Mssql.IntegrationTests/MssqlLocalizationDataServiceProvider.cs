using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;

namespace FluiTec.AppFx.Localization.Dapper.Mssql.IntegrationTests;

/// <summary>
///     A mssql localization data service provider.
/// </summary>
public class
    MssqlLocalizationDataServiceProvider : MssqlDataServiceProvider<ILocalizationDataService,
        ILocalizationUnitOfWork>
{
    /// <summary>
    ///     Provide data service.
    /// </summary>
    /// <returns>
    ///     A TDataService.
    /// </returns>
    public override ILocalizationDataService ProvideDataService()
    {
        return new MssqlLocalizationDataService(ServiceOptions, null);
    }
}