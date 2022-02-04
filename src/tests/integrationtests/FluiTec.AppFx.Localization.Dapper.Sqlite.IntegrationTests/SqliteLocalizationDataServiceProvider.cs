using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;

namespace FluiTec.AppFx.Localization.Dapper.Sqlite.IntegrationTests;

/// <summary>
///     A database provider.
/// </summary>
internal class
    SqliteLocalizationDataServiceProvider : SqliteDataServiceProvider<ILocalizationDataService, ILocalizationUnitOfWork>
{
    /// <summary>
    ///     Provide data service.
    /// </summary>
    /// <returns>
    ///     A TDataService.
    /// </returns>
    public override ILocalizationDataService ProvideDataService()
    {
        return new SqliteLocalizationDataService(ServiceOptions, null);
    }
}