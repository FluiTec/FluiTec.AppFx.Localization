using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;

namespace FluiTec.AppFx.Localization.Dapper.Mysql.IntegrationTests;

/// <summary>
///     A mssql localization data service provider.
/// </summary>
public class
    MysqlLocalizationDataServiceProvider : MysqlDataServiceProvider<ILocalizationDataService,
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
        return new MysqlLocalizationDataService(ServiceOptions, null);
    }
}