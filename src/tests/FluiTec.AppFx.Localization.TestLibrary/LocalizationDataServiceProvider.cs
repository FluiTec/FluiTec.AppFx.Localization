namespace FluiTec.AppFx.Localization.TestLibrary
{
    /// <summary>
    ///     A data service provider.
    /// </summary>
    public abstract class LocalizationDataServiceProvider
    {
        /// <summary>
        ///     Gets a value indicating whether the database is available.
        /// </summary>
        /// <value>
        ///     True if the database is available, false if not.
        /// </value>
        public abstract bool IsDbAvailable { get; }

        /// <summary>
        ///     Provider data service.
        /// </summary>
        /// <returns>
        ///     An ILocalizationDataService.
        /// </returns>
        public abstract ILocalizationDataService ProvideDataService();
    }
}