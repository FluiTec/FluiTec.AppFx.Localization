using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace FluiTec.AppFx.Localization
{
    /// <summary>
    /// A database string localizer.
    /// </summary>
    public class DbStringLocalizer : IStringLocalizer
    {
        public ILocalizationDataService LocalizationDataService { get; }

        public ILogger<DbStringLocalizer> Logger { get; }

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



            throw new NotImplementedException();
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