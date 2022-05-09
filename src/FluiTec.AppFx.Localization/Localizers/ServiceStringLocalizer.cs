using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Localization.Models;
using FluiTec.AppFx.Localization.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization.Localizers
{
    /// <summary>
    ///     A service string localizer.
    /// </summary>
    public class ServiceStringLocalizer : IStringLocalizer
    {
        /// <summary>
        ///     (Immutable) the culture.
        /// </summary>
        private readonly CultureInfo _culture;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService, ILogger<ServiceStringLocalizer> logger)
            : this(localizationService, null, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService, CultureInfo culture,
            ILogger<ServiceStringLocalizer> logger)
            : this(string.Empty, localizationService, culture, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="resource">             The resource. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(Type resource, ILocalizationService localizationService,
            ILogger<ServiceStringLocalizer> logger)
            : this(resource.FullName, localizationService, null, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="baseName">             The name of the base. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService,
            ILogger<ServiceStringLocalizer> logger)
            : this(baseName, localizationService, null, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="resource">             The resource. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(Type resource, ILocalizationService localizationService, CultureInfo culture,
            ILogger<ServiceStringLocalizer> logger)
            : this(resource.FullName, localizationService, culture, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when one or more required arguments are
        ///     null.
        /// </exception>
        /// <param name="baseName">             The name of the base. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService, CultureInfo culture,
            ILogger<ServiceStringLocalizer> logger)
        {
            BaseName = baseName;
            HasBaseName = !string.IsNullOrWhiteSpace(baseName);
            LocalizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
            Logger = logger;
            _culture = culture;
            LocalizedStrings = new Dictionary<CultureInfo, List<LocalizedStringEx>>();

            Logger?.LogTrace("New ServiceStringLocalizer / (BaseName='{BaseName}', Culture='{culture}')", BaseName, culture);
        }

        /// <summary>
        ///     Gets the name of the base.
        /// </summary>
        /// <value>
        ///     The name of the base.
        /// </value>
        public string BaseName { get; }

        /// <summary>
        ///     Gets a value indicating whether this object has base name.
        /// </summary>
        /// <value>
        ///     True if this object has base name, false if not.
        /// </value>
        public bool HasBaseName { get; }

        /// <summary>
        ///     Gets the localization service.
        /// </summary>
        /// <value>
        ///     The localization service.
        /// </value>
        public ILocalizationService LocalizationService { get; }

        /// <summary>
        ///     Gets the logger.
        /// </summary>
        /// <value>
        ///     The logger.
        /// </value>
        public ILogger<ServiceStringLocalizer> Logger { get; }

        /// <summary>
        ///     Gets the culture.
        /// </summary>
        /// <value>
        ///     The culture.
        /// </value>
        public CultureInfo Culture => _culture ?? CultureInfo.CurrentUICulture;

        /// <summary>
        ///     Gets the localized strings.
        /// </summary>
        /// <value>
        ///     The localized strings.
        /// </value>
        protected Dictionary<CultureInfo, List<LocalizedStringEx>> LocalizedStrings { get; }

        /// <summary>
        ///     Gets all string resources.
        /// </summary>
        /// <param name="includeParentCultures">
        ///     A <see cref="T:System.Boolean" /> indicating whether
        ///     to include strings from parent cultures.
        /// </param>
        /// <returns>
        ///     The strings.
        /// </returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            Logger?.LogTrace("GetAllStrings({includeParentCultures}) / (BaseName='{BaseName}', Culture='{culture}'))", includeParentCultures, BaseName, Culture);

            if (LocalizedStrings.ContainsKey(Culture))
            {
                Logger?.LogTrace("-> Return from ClassCache. ({count} items) / ", LocalizedStrings[Culture].Count);
                return LocalizedStrings[Culture];
            }

            LocalizedStrings.Add(Culture, GetLocalizedStrings(Culture));
            Logger?.LogTrace("-> fill ClassCache. ({count} items)", LocalizedStrings[Culture].Count);
            return LocalizedStrings[Culture];
        }

        /// <summary>
        ///     Creates a new <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> for a
        ///     specific <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <param name="culture">  The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        /// <returns>
        ///     A culture-specific <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return HasBaseName
                ? new ServiceStringLocalizer(BaseName, LocalizationService, culture, Logger)
                : new ServiceStringLocalizer(LocalizationService, culture, Logger);
        }

        /// <summary>
        ///     Gets the string resource with the given name.
        /// </summary>
        /// <param name="name"> The name of the string resource. </param>
        /// <returns>
        ///     The string resource as a <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        public LocalizedString this[string name]
        {
            get
            {
                Logger?.LogTrace("Index by Name = '{name}'", HasBaseName ? $"{BaseName}.{name}" : name);
                if (!HasBaseName) return LocalizationService.ByName(name, Culture);

                var fName = $"{BaseName}.{name}";

                if (!LocalizedStrings.ContainsKey(Culture))
                {
                    Logger?.LogTrace("missing strings for culture='{culture}'", Culture);
                    var strings = GetLocalizedStrings(Culture);
                    LocalizedStrings.Add(Culture, strings);
                    Logger?.LogTrace("add strings for culture='{culture}' ({count} items)", Culture, strings.Count);
                }

                var existing = LocalizedStrings[Culture].SingleOrDefault(s => s.Name == fName);
                return existing ?? LocalizationService.ByNameNotFound(name, Culture);
            }
        }

        /// <summary>
        ///     Gets the string resource with the given name.
        /// </summary>
        /// <param name="name">         The name of the string resource. </param>
        /// <param name="arguments">    A variable-length parameters list containing arguments. </param>
        /// <returns>
        ///     The string resource as a <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var localized = this[name];
                return localized.ResourceNotFound
                    ? localized
                    : new LocalizedString(name, string.Format(localized.Value, arguments));
            }
        }

        /// <summary>
        ///     Gets localized strings.
        /// </summary>
        /// <param name="culture">  The culture. </param>
        /// <returns>
        ///     The localized strings.
        /// </returns>
        protected List<LocalizedStringEx> GetLocalizedStrings(CultureInfo culture)
        {
            return HasBaseName
                ? LocalizationService.ByBaseName(BaseName, culture).ToList()
                : new List<LocalizedStringEx>();
        }
    }

    /// <summary>
    ///     A service string localizer.
    /// </summary>
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public class ServiceStringLocalizer<T> : ServiceStringLocalizer, IStringLocalizer<T>
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService, ILogger<ServiceStringLocalizer> logger)
            : base(typeof(T), localizationService, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService, CultureInfo culture,
            ILogger<ServiceStringLocalizer> logger) : base(localizationService, culture, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="resource">             The resource. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(Type resource, ILocalizationService localizationService,
            ILogger<ServiceStringLocalizer> logger) : base(resource, localizationService, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="baseName">             The name of the base. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService,
            ILogger<ServiceStringLocalizer> logger) : base(baseName, localizationService, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="resource">             The resource. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(Type resource, ILocalizationService localizationService, CultureInfo culture,
            ILogger<ServiceStringLocalizer> logger) : base(resource, localizationService, culture, logger)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="baseName">             The name of the base. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        /// <param name="logger">               The logger. </param>
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService, CultureInfo culture,
            ILogger<ServiceStringLocalizer> logger) : base(baseName, localizationService, culture, logger)
        {
        }
    }
}