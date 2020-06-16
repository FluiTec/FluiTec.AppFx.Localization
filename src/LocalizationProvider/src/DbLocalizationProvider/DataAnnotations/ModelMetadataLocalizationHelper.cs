// Copyright © 2017 Valdis Iljuconoks.
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

// Changes by Achim Schnell:
// 1. Changed to public
// 2. Changed GetTranslation to public
// 3. Changed UseLegacyMode to public
// Reason: Allow others to implement custom DataAccessLayer

using System;
using FluiTec.DbLocalizationProvider.Internal;

namespace FluiTec.DbLocalizationProvider.DataAnnotations
{
    /// <summary>   A model metadata localization helper. </summary>
    public class ModelMetadataLocalizationHelper
    {
        /// <summary>   The use legacy mode. </summary>
        public static Func<string, bool> UseLegacyMode =
            x => !string.IsNullOrWhiteSpace(x) && x.StartsWith("/") &&
                 ConfigurationContext.Current.ModelMetadataProviders.EnableLegacyMode();

        /// <summary>   Gets a translation. </summary>
        /// <param name="resourceKey">  The resource key. </param>
        /// <returns>   The translation. </returns>
        public static string GetTranslation(string resourceKey)
        {
            var result = resourceKey;
            if (!ConfigurationContext.Current.EnableLocalization())
                return result;

            var localizedDisplayName = LocalizationProvider.Current.GetString(resourceKey);
            result = localizedDisplayName;

            if (!ConfigurationContext.Current.ModelMetadataProviders.EnableLegacyMode())
                return result;

            // for the legacy purposes - we need to look for this resource value as resource translation
            // once again - this will make sure that existing XPath resources are still working
            if (UseLegacyMode(localizedDisplayName))
                result = LocalizationProvider.Current.GetString(localizedDisplayName);

            // If other data annotations exists execept for [Display], an exception is thrown when displayname is ""
            // It should be null to avoid exception as ModelMetadata.GetDisplayName only checks for null and not String.Empty
            return string.IsNullOrWhiteSpace(localizedDisplayName) ? null : result;
        }

        /// <summary>   Gets a translation. </summary>
        /// <param name="containerType">    Type of the container. </param>
        /// <param name="propertyName">     Name of the property. </param>
        /// <returns>   The translation. </returns>
        public static string GetTranslation(Type containerType, string propertyName)
        {
            var resourceKey = ResourceKeyBuilder.BuildResourceKey(containerType, propertyName);
            return GetTranslation(resourceKey);
        }
    }
}