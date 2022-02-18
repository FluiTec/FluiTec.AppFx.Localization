using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Exceptions;
using FluiTec.AppFx.Localization.Models;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Services
{
    /// <summary>
    ///     A service for accessing data localizations information.
    /// </summary>
    public class DataLocalizationService : ILocalizationService
    {
        #region Properties

        /// <summary>
        /// Gets the name of the source.
        /// </summary>
        ///
        /// <value>
        /// The name of the source.
        /// </value>
        protected virtual string SourceName { get; }

        /// <summary>
        ///     Gets the data service.
        /// </summary>
        /// <value>
        ///     The data service.
        /// </value>
        public ILocalizationDataService DataService { get; }

        /// <summary>
        /// Gets the translation picking service.
        /// </summary>
        ///
        /// <value>
        /// The translation picking service.
        /// </value>
        public ITranslationPickingService TranslationPickingService { get; }

        /// <summary>
        /// Gets options for controlling the operation.
        /// </summary>
        ///
        /// <value>
        /// The options.
        /// </value>
        public ServiceLocalizationOptions Options { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        ///
        /// <value>
        /// The logger.
        /// </value>
        public ILogger<DataLocalizationService> Logger { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="dataService">                  The data service. </param>
        /// <param name="translationPickingService">    The translation picking service. </param>
        /// <param name="options">                      Options for controlling the operation. </param>
        /// <param name="logger">                       The logger. </param>
        public DataLocalizationService(ILocalizationDataService dataService, ITranslationPickingService translationPickingService, ServiceLocalizationOptions options, ILogger<DataLocalizationService> logger)
        {
            DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            TranslationPickingService = translationPickingService ?? throw new ArgumentNullException(nameof(translationPickingService));
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Logger = logger;
            SourceName = DataService.Name;
        }

        #endregion

        #region ILocalizationService
        
        /// <summary>
        ///     By name.
        /// </summary>
        /// <param name="name">     The name. </param>
        /// <param name="culture">  The culture. </param>
        /// <returns>
        ///     A LocalizedString.
        /// </returns>
        public virtual LocalizedStringEx ByName(string name, CultureInfo culture)
        {
            using var uow = DataService.BeginUnitOfWork();

            var translations = uow.TranslationRepository
                .GetByResourceCompound(name)
                .ToList();

            return TranslationPickingService.Pick(translations, culture, SourceName)
                   ?? CreateNotFoundLocalizedString(name, culture);
        }

        /// <summary>
        ///     Enumerates by base name in this collection.
        /// </summary>
        /// <param name="baseName"> Name of the base. </param>
        /// <param name="culture">  The culture. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process by base name in this collection.
        /// </returns>
        public virtual IEnumerable<LocalizedStringEx> ByBaseName(string baseName, CultureInfo culture)
        {
            using var uow = DataService.BeginUnitOfWork();

            var translations = uow.TranslationRepository
                    .GetByResourceSuffixCompound(baseName)
                    .OrderBy(t => t.Resource.ResourceKey)
                    .GroupBy(t => t.Resource.ResourceKey)
                    .ToList();

            return TranslationPickingService.Pick(translations, culture, SourceName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates not found localized string.
        /// </summary>
        ///
        /// <exception cref="MissingLocalizationException"> Thrown when a Missing Localization error
        ///                                                 condition occurs. </exception>
        /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
        ///                                                 the required range. </exception>
        ///
        /// <param name="name">     The name. </param>
        /// <param name="culture">  The culture. </param>
        ///
        /// <returns>
        /// The new not found localized string.
        /// </returns>
        protected LocalizedStringEx CreateNotFoundLocalizedString(string name, CultureInfo culture)
        {
            switch (Options.MissingLocalizationBehavior)
            {
                case MissingLocalizationBehavior.Ignore:
                    return new LocalizedStringEx(name, name, true, SourceName, !string.IsNullOrWhiteSpace(culture.Name) ? culture.Name : culture.TwoLetterISOLanguageName);
                case MissingLocalizationBehavior.Log:
                    Logger?.LogWarning($"Missing localization, name: {name}, culture: {culture.Name}|{culture.TwoLetterISOLanguageName}");
                    return new LocalizedStringEx(name, name, true, SourceName, !string.IsNullOrWhiteSpace(culture.Name) ? culture.Name : culture.TwoLetterISOLanguageName);
                case MissingLocalizationBehavior.Throw:
                    throw new MissingLocalizationException(name, culture);
                default:
                    throw new ArgumentOutOfRangeException(nameof(Options.MissingLocalizationBehavior));
            }
            
        }

        #endregion
    }
}