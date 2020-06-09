using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.DbLocalizationProvider;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization
{
    /// <summary>A database HTML localizer.</summary>
    public class DbHtmlLocalizer : IViewLocalizer
    {
        /// <summary>Name of the base.</summary>
        private readonly string _baseName;

        /// <summary>   The culture. </summary>
        private readonly CultureInfo _culture;

        /// <summary>The location.</summary>
        private readonly string _location;

        /// <summary>Type of the resource.</summary>
        private readonly Type _resourceType;

        /// <summary>Gets the string resource with the given name.</summary>
        /// <param name="name"> The name of the string resource. </param>
        /// <returns>
        ///     The string resource as a
        ///     <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        public LocalizedString GetString(string name)
        {
            var value = LocalizationProvider.Current.GetStringByCulture(name, _culture);
            return new LocalizedString(name, value ?? name, value == null);
        }

        /// <summary>
        ///     Gets the string resource with the given name and formatted with the supplied
        ///     arguments.
        /// </summary>
        /// <param name="name">         The name of the string resource. </param>
        /// <param name="arguments">    The values to format the string with. </param>
        /// <returns>
        ///     The formatted string resource as a
        ///     <see cref="T:Microsoft.Extensions.Localization.LocalizedString" />.
        /// </returns>
        public LocalizedString GetString(string name, params object[] arguments)
        {
            var value = LocalizationProvider.Current.GetStringByCulture(name, _culture, arguments);
            return new LocalizedString(name, value ?? name, value == null);
        }

        /// <summary>Gets all string resources.</summary>
        /// <param name="includeParentCultures">
        ///     A <see cref="T:System.Boolean" /> indicating whether
        ///     to include strings from parent cultures.
        /// </param>
        /// <returns>The strings.</returns>
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return Enumerable.Empty<LocalizedString>();
        }

        /// <summary>
        ///     Creates a new <see cref="T:Microsoft.AspNetCore.Mvc.Localization.IHtmlLocalizer" />
        ///     for a specific <see cref="T:System.Globalization.CultureInfo" />.
        /// </summary>
        /// <param name="culture">  The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        /// <returns>
        ///     A culture-specific
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.Localization.IHtmlLocalizer" />.
        /// </returns>
        public IHtmlLocalizer WithCulture(CultureInfo culture)
        {
            if (_resourceType != null)
                return new DbHtmlLocalizer(_resourceType, culture);
            if (!string.IsNullOrWhiteSpace(_baseName) && !string.IsNullOrWhiteSpace(_location))
                return new DbHtmlLocalizer(_baseName, _location, culture);
            return new DbHtmlLocalizer(culture);
        }

        /// <summary>Gets the string resource with the given name.</summary>
        /// <param name="name"> The name of the string resource. </param>
        /// <returns>
        ///     The string resource as a
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.Localization.LocalizedHtmlString" />.
        /// </returns>
        public LocalizedHtmlString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedHtmlString(name, value);
            }
        }

        /// <summary>Gets the string resource with the given name.</summary>
        /// <param name="name">         The name of the string resource. </param>
        /// <param name="arguments">    A variable-length parameters list containing arguments. </param>
        /// <returns>
        ///     The string resource as a
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.Localization.LocalizedHtmlString" />.
        /// </returns>
        public LocalizedHtmlString this[string name, params object[] arguments]
        {
            get
            {
                var value = GetString(name, arguments);
                return new LocalizedHtmlString(name, value);
            }
        }

        #region Constructors

        /// <summary>   Default constructor. </summary>
        protected DbHtmlLocalizer()
        {
            _culture = CultureInfo.CurrentUICulture;
        }

        /// <summary>Constructor.</summary>
        /// <param name="resourceType"> Type of the resource. </param>
        public DbHtmlLocalizer(Type resourceType) : this()
        {
            _resourceType = resourceType;
        }

        /// <summary>Constructor.</summary>
        /// <param name="resourceType"> Type of the resource. </param>
        /// <param name="culture">      The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        public DbHtmlLocalizer(Type resourceType, CultureInfo culture) : this(culture)
        {
            _resourceType = resourceType;
        }

        /// <summary>   Constructor. </summary>
        /// <param name="culture">  The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        protected DbHtmlLocalizer(CultureInfo culture) : this()
        {
            _culture = culture;
        }

        /// <summary>Constructor.</summary>
        /// <param name="baseName"> Name of the base. </param>
        /// <param name="location"> The location. </param>
        public DbHtmlLocalizer(string baseName, string location) : this()
        {
            _baseName = baseName;
            _location = location;
        }

        /// <summary>Constructor.</summary>
        /// <param name="baseName"> Name of the base. </param>
        /// <param name="location"> The location. </param>
        /// <param name="culture">  The <see cref="T:System.Globalization.CultureInfo" /> to use. </param>
        public DbHtmlLocalizer(string baseName, string location, CultureInfo culture) : this(culture)
        {
            _baseName = baseName;
            _location = location;
        }

        #endregion
    }
}