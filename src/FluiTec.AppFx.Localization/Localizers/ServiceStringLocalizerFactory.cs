using System;
using System.Globalization;
using FluiTec.AppFx.Localization.Services;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.Localizers
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
            return new ServiceStringLocalizer(resourceSource, LocalizationService);
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
            return new ServiceStringLocalizer(baseName, LocalizationService);
        }
    }
}