using FluiTec.AppFx.Data.TestLibrary.DataServiceProviders;

namespace FluiTec.AppFx.Localization.NMemory.IntegrationTests
{
    /// <summary>
    ///     A memory data service provider.
    /// </summary>
    public class NMemoryDataServiceProvider : DataServiceProvider<ILocalizationDataService, ILocalizationUnitOfWork>
    {
        /// <summary>
        ///     Gets a value indicating whether the database is available.
        /// </summary>
        /// <value>
        ///     True if the database is available, false if not.
        /// </value>
        public override bool IsDbAvailable => true;

        /// <summary>
        ///     Provider data service.
        /// </summary>
        /// <returns>
        ///     An ILocalizationDataService.
        /// </returns>
        public override ILocalizationDataService ProvideDataService()
        {
            return new NMemoryLocalizationDataService(null);
        }
    }
}