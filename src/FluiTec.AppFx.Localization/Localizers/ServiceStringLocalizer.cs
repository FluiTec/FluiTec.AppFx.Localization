using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Localization.Models;
using FluiTec.AppFx.Localization.Services;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.Localizers
{
    /// <summary>
    ///     A service string localizer.
    /// </summary>
    public class ServiceStringLocalizer : IStringLocalizer
    {
        /// <summary>
        /// (Immutable) the culture.
        /// </summary>
        private readonly CultureInfo _culture;

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService)
            : this(localizationService, null)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService, CultureInfo culture)
            : this(string.Empty, localizationService, culture)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="resource">             The resource. </param>
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizer(Type resource, ILocalizationService localizationService)
            : this(resource.FullName, localizationService, null)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="baseName">             The name of the base. </param>
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService)
            : this(baseName, localizationService, null)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="resource">             The resource. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        public ServiceStringLocalizer(Type resource, ILocalizationService localizationService, CultureInfo culture)
            : this(resource.FullName, localizationService, culture)
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
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService, CultureInfo culture)
        {
            BaseName = baseName;
            HasBaseName = !string.IsNullOrWhiteSpace(baseName);
            LocalizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
            _culture = culture;

            LocalizedStrings = new Dictionary<CultureInfo, List<LocalizedStringEx>>();
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
            if (LocalizedStrings.ContainsKey(Culture))
                return LocalizedStrings[Culture];

            LocalizedStrings.Add(Culture, GetLocalizedStrings(Culture));
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
                ? new ServiceStringLocalizer(BaseName, LocalizationService, culture)
                : new ServiceStringLocalizer(LocalizationService, culture);
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
                if (!HasBaseName) return LocalizationService.ByName(name, Culture);

                var fName = $"{BaseName}.{name}";

                if (!LocalizedStrings.ContainsKey(Culture))
                    LocalizedStrings.Add(Culture, GetLocalizedStrings(Culture));

                var existing = LocalizedStrings[Culture].SingleOrDefault(s => s.Name == fName);
                return existing ?? LocalizationService.ByName(name, Culture);
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
        /// Gets localized strings.
        /// </summary>
        ///
        /// <param name="culture">  The culture. </param>
        ///
        /// <returns>
        /// The localized strings.
        /// </returns>
        protected List<LocalizedStringEx> GetLocalizedStrings(CultureInfo culture)
        {
            return HasBaseName 
                ? LocalizationService.ByBaseName(BaseName, culture).ToList() 
                : new List<LocalizedStringEx>();
        }
    }

    /// <summary>
    /// A service string localizer.
    /// </summary>
    ///
    /// <typeparam name="T">    Generic type parameter. </typeparam>
    public class ServiceStringLocalizer<T> : ServiceStringLocalizer, IStringLocalizer<T>
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService) : base(typeof(T), localizationService)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService, CultureInfo culture) : base(localizationService, culture)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="resource">             The resource. </param>
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizer(Type resource, ILocalizationService localizationService) : base(resource, localizationService)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="baseName">             The name of the base. </param>
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService) : base(baseName, localizationService)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="resource">             The resource. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        public ServiceStringLocalizer(Type resource, ILocalizationService localizationService, CultureInfo culture) : base(resource, localizationService, culture)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="baseName">             The name of the base. </param>
        /// <param name="localizationService">  The localization service. </param>
        /// <param name="culture">              The culture. </param>
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService, CultureInfo culture) : base(baseName, localizationService, culture)
        {
        }
    }
}