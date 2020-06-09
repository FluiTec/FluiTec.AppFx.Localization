using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace FluiTec.AppFx.Localization
{
    /// <summary>   A localized display metadata provider. </summary>
    public class LocalizedDisplayMetadataProvider : IDisplayMetadataProvider
    {
        /// <summary>
        ///     Sets the values for properties of
        ///     <see cref="P:Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DisplayMetadataProviderContext.DisplayMetadata" />.
        /// </summary>
        /// <param name="context">
        ///     The
        ///     <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.Metadata.DisplayMetadataProviderContext" />.
        /// </param>
        public void CreateDisplayMetadata(DisplayMetadataProviderContext context)
        {
            var theAttributes = context.Attributes;
            var modelMetadata = context.DisplayMetadata;
            var propertyName = context.Key.Name;
            var containerType = context.Key.ContainerType;

            if (containerType == null)
                return;

            if (containerType.GetCustomAttribute<LocalizedModelAttribute>() == null)
                return;

            var currentMetaData = modelMetadata.DisplayName?.Invoke();

            modelMetadata.DisplayName = () => !ModelMetadataLocalizationHelper.UseLegacyMode(currentMetaData)
                ? ModelMetadataLocalizationHelper.GetTranslation(containerType, propertyName)
                : ModelMetadataLocalizationHelper.GetTranslation(currentMetaData);

            var displayAttribute = theAttributes.OfType<DisplayAttribute>().FirstOrDefault();
            if (displayAttribute?.Description != null)
                // ReSharper disable once ImplicitlyCapturedClosure
                modelMetadata.Description = () =>
                    ModelMetadataLocalizationHelper.GetTranslation(containerType, $"{propertyName}-Description");
        }
    }
}