using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluiTec.AppFx.Localization.Configuration;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    /// A service for accessing localization imports information.
    /// </summary>
    public class LocalizationImportService : ILocalizationImportService
    {
        /// <summary>
        /// Gets the data service.
        /// </summary>
        ///
        /// <value>
        /// The data service.
        /// </value>
        public ILocalizationDataService DataService { get; }

        /// <summary>
        /// Gets options for controlling the operation.
        /// </summary>
        ///
        /// <value>
        /// The options.
        /// </value>
        public ServiceLocalizationImportOptions Options { get; }

        /// <summary>
        /// Gets the localization sources.
        /// </summary>
        ///
        /// <value>
        /// The localization sources.
        /// </value>
        public IEnumerable<ILocalizationSource> LocalizationSources { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dataService">          The data service. </param>
        /// <param name="options">              The options. </param>
        /// <param name="localizationSources">  The localization sources. </param>
        public LocalizationImportService(ILocalizationDataService dataService, ServiceLocalizationImportOptions options, 
            IEnumerable<ILocalizationSource> localizationSources)
        {
            DataService = dataService;
            Options = options;
            LocalizationSources = localizationSources;
        }

        /// <summary>
        /// Gets the import.
        /// </summary>
        ///
        /// <returns>
        /// An int.
        /// </returns>
        public int Import()
        {
            foreach (var source in LocalizationSources)
            {
                var resources = source.FindResources().ToList();
            }

            return 0;
        }

        /// <summary>
        /// Import asynchronous.
        /// </summary>
        ///
        /// <returns>
        /// The import.
        /// </returns>
        public Task<int> ImportAsync()
        {
            foreach (var source in LocalizationSources)
            {
                var resources = source.FindResources();
            }

            return Task.FromResult(0);
        }
    }
}