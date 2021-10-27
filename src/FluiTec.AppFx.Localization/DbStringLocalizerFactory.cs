using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization
{
    /// <summary>
    /// A database string localizer factory.
    /// </summary>
    public class DbStringLocalizerFactory : IStringLocalizerFactory
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
        /// Gets the logger factory.
        /// </summary>
        ///
        /// <value>
        /// The logger factory.
        /// </value>
        public ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="dataService">      The data service. </param>
        /// <param name="loggerFactory">    The logger factory. </param>
        public DbStringLocalizerFactory(ILocalizationDataService dataService, ILoggerFactory loggerFactory)
        {
            DataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
            LoggerFactory = loggerFactory;
        }

        /// <summary>
        /// Creates an <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> using the
        /// <see cref="T:System.Reflection.Assembly" /> and
        /// <see cref="P:System.Type.FullName" /> of the specified <see cref="T:System.Type" />.
        /// </summary>
        ///
        /// <param name="resourceSource">   The <see cref="T:System.Type" />. </param>
        ///
        /// <returns>
        /// The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            return new DbStringLocalizer(DataService, LoggerFactory?.CreateLogger<DbStringLocalizer>());
        }

        /// <summary>
        /// Creates an <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </summary>
        ///
        /// <param name="baseName"> The base name of the resource to load strings from. </param>
        /// <param name="location"> The location to load resources from. </param>
        ///
        /// <returns>
        /// The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            return new DbStringLocalizer(DataService, LoggerFactory?.CreateLogger<DbStringLocalizer>());
        }
    }

    /// <summary>
    /// A database string localizer.
    /// </summary>
    public class DbStringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// Gets the localization data service.
        /// </summary>
        ///
        /// <value>
        /// The localization data service.
        /// </value>
        public ILocalizationDataService LocalizationDataService { get; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        ///
        /// <value>
        /// The logger.
        /// </value>
        public ILogger<DbStringLocalizer> Logger { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="localizationDataService">  The localization data service. </param>
        /// <param name="logger">                   The logger. </param>
        public DbStringLocalizer(ILocalizationDataService localizationDataService, ILogger<DbStringLocalizer> logger)
        {
            LocalizationDataService = localizationDataService;
            Logger = logger;
        }

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        ///
        /// <param name="includeParentCultures">    A <see cref="T:System.Boolean" /> indicating whether
        ///                                         to include strings from parent cultures. </param>
        ///
        /// <returns>
        /// The strings.
        /// </returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
            => GetAllStrings(includeParentCultures, CultureInfo.CurrentUICulture);

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="includeParentCultures">    A <see cref="T:System.Boolean" /> indicating whether
        ///                                         to include strings from parent cultures. </param>
        /// <param name="culture">                  The <see cref="T:System.Globalization.CultureInfo" />
        ///                                         to use. </param>
        ///
        /// <returns>
        /// The strings.
        /// </returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures, CultureInfo culture)
        {
            if (culture == null) throw new ArgumentNullException(nameof(culture));

            using var uow = LocalizationDataService.BeginUnitOfWork();

            var languages = !includeParentCultures 
                ? new[] { uow.LanguageRepository.Get(culture.Name) } 
                : uow.LanguageRepository.GetByTwoLetterIso(culture.TwoLetterISOLanguageName);

            var translations = uow.TranslationRepository
                .GetByLanguages(languages)
                .GroupBy(t => t.Resource.ResourceKey)
                .Select(g =>
                    g.Aggregate((e1, e2)
                        => e1.Language.IsoName.Length > e2.Language.IsoName.Length ? e1 : e2));

            return translations
                .Select(t 
                    => new LocalizedString(t.Resource.ResourceKey, t.Translation.Value))
                .ToList();
        }

        /// <summary>
        /// Creates a new <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> for a
        /// specific <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        ///
        /// <param name="culture">  The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        ///
        /// <returns>
        /// A culture-specific <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        ///
        /// <param name="name"> The name of the string resource. </param>
        ///
        /// <returns>
        /// The string resource as a <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        public LocalizedString this[string name] => throw new System.NotImplementedException();

        /// <summary>
        /// Gets the string resource with the given name.
        /// </summary>
        ///
        /// <param name="name">         The name of the string resource. </param>
        /// <param name="arguments">    A variable-length parameters list containing arguments. </param>
        ///
        /// <returns>
        /// The string resource as a <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        public LocalizedString this[string name, params object[] arguments] => throw new System.NotImplementedException();
    }
}