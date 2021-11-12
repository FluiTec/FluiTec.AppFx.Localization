using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Localization.Services;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization
{
    /// <summary>
    ///     A string localizer factory.
    /// </summary>
    public class ServiceStringLocalizerFactory : IStringLocalizerFactory
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizerFactory(ILocalizationService localizationService)
        {
            LocalizationService = localizationService;
        }

        /// <summary>
        ///     Gets the localization service.
        /// </summary>
        /// <value>
        ///     The localization service.
        /// </value>
        public ILocalizationService LocalizationService { get; }

        /// <summary>
        ///     Creates an <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" /> using the
        ///     <see cref="T:System.Reflection.Assembly" /> and
        ///     <see cref="P:System.Type.FullName" /> of the specified <see cref="T:System.Type" />.
        /// </summary>
        /// <param name="resourceSource">   The <see cref="T:System.Type" />. </param>
        /// <returns>
        ///     The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            return new ServiceStringLocalizer(resourceSource, LocalizationService, CultureInfo.CurrentUICulture);
        }

        /// <summary>
        ///     Creates an <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </summary>
        /// <param name="baseName"> The base name of the resource to load strings from. </param>
        /// <param name="location"> The location to load resources from. </param>
        /// <returns>
        ///     The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            return new ServiceStringLocalizer(baseName, LocalizationService, CultureInfo.CurrentUICulture);
        }
    }

    /// <summary>
    ///     A service string localizer.
    /// </summary>
    public class ServiceStringLocalizer : IStringLocalizer
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizer(ILocalizationService localizationService)
            : this(localizationService, CultureInfo.CurrentUICulture)
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
            : this(resource.FullName, localizationService, CultureInfo.CurrentUICulture)
        {
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="baseName">             The name of the base. </param>
        /// <param name="localizationService">  The localization service. </param>
        public ServiceStringLocalizer(string baseName, ILocalizationService localizationService)
            : this(baseName, localizationService, CultureInfo.CurrentUICulture)
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
            Culture = culture ?? CultureInfo.CurrentUICulture;

            LocalizedStrings = new Lazy<List<LocalizedString>>(() => HasBaseName
                ? localizationService.ByBaseName(BaseName, Culture).ToList()
                : new List<LocalizedString>());
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
        public CultureInfo Culture { get; }

        /// <summary>
        ///     Gets the localized strings.
        /// </summary>
        /// <value>
        ///     The localized strings.
        /// </value>
        protected Lazy<List<LocalizedString>> LocalizedStrings { get; }

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
            return LocalizedStrings.Value;
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
                var existing = LocalizedStrings.Value.SingleOrDefault(s => s.Name == name);
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
    }
}