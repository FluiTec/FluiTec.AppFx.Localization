using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.Services
{
    /// <summary>
    /// A service for accessing data localizations information.
    /// </summary>
    public class DataLocalizationService : ILocalizationService
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
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dataService">  The data service. </param>
        public DataLocalizationService(ILocalizationDataService dataService)
        {
            DataService = dataService;
        }

        /// <summary>
        /// By name.
        /// </summary>
        ///
        /// <param name="name">     The name. </param>
        /// <param name="culture">  The culture. </param>
        ///
        /// <returns>
        /// A LocalizedString.
        /// </returns>
        public virtual LocalizedString ByName(string name, CultureInfo culture)
        {
            using var uow = DataService.BeginUnitOfWork();
            var resource = uow.ResourceRepository.Get(name);

            if (resource == null)
                return new LocalizedString(name, name, true);

            var translations = uow.TranslationRepository
                .GetByResourceCompound(resource)
                .ToList();

            var perfectFit = translations.SingleOrDefault(t => t.Language.IsoName == culture.Name);
            if (perfectFit != null) return new LocalizedString(name, perfectFit.Translation.Value);

            var baseFit =
                translations.SingleOrDefault(t => t.Language.IsoName[..2] == culture.TwoLetterISOLanguageName);
            if (baseFit != null) return new LocalizedString(name, baseFit.Translation.Value);

            return new LocalizedString(name, name, true);
        }

        /// <summary>
        /// Enumerates by base name in this collection.
        /// </summary>
        ///
        /// <param name="baseName"> Name of the base. </param>
        /// <param name="culture">  The culture. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process by base name in this collection.
        /// </returns>
        public virtual IEnumerable<LocalizedString> ByBaseName(string baseName, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}