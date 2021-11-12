using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;

namespace FluiTec.AppFx.Localization.Dapper.Pgsql.IntegrationTests;

/// <summary>
///     A mssql localization data service provider.
/// </summary>
public class
    PgsqlLocalizationDataServiceProvider : PgsqlDataServiceProvider<ILocalizationDataService,
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
        return new PgsqlLocalizationDataService(ServiceOptions, null);
    }
}