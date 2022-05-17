using System;
using FluiTec.AppFx.Localization.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

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
        /// <param name="loggerFactory">        The logger factory. </param>
        public ServiceStringLocalizerFactory(ILocalizationService localizationService, ILoggerFactory loggerFactory)
        {
            LocalizationService = localizationService;
            LoggerFactory = loggerFactory;
        }

        /// <summary>
        ///     Gets the localization service.
        /// </summary>
        /// <value>
        ///     The localization service.
        /// </value>
        public ILocalizationService LocalizationService { get; }

        /// <summary>
        ///     Gets the logger factory.
        /// </summary>
        /// <value>
        ///     The logger factory.
        /// </value>
        public ILoggerFactory LoggerFactory { get; }

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
            return new ServiceStringLocalizer(resourceSource, LocalizationService,
                LoggerFactory?.CreateLogger<ServiceStringLocalizer>());
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
            return new ServiceStringLocalizer(baseName, LocalizationService,
                LoggerFactory?.CreateLogger<ServiceStringLocalizer>());
        }
    }
}