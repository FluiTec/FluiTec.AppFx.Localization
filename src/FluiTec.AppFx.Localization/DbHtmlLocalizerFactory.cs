using System;
using Microsoft.AspNetCore.Mvc.Localization;

namespace FluiTec.AppFx.Localization
{
    /// <summary>A database HTML localizer factory.</summary>
    public class DbHtmlLocalizerFactory : IHtmlLocalizerFactory
    {
        /// <summary>
        ///     Creates an <see cref="T:Microsoft.AspNetCore.Mvc.Localization.IHtmlLocalizer" />
        ///     using the <see cref="T:System.Reflection.Assembly" /> and
        ///     <see cref="P:System.Type.FullName" /> of the specified <see cref="T:System.Type" />.
        /// </summary>
        /// <param name="resourceType"> Type of the resource. </param>
        /// <returns>The <see cref="T:Microsoft.AspNetCore.Mvc.Localization.IHtmlLocalizer" />.</returns>
        public IHtmlLocalizer Create(Type resourceType)
        {
            return new DbHtmlLocalizer(resourceType);
        }

        /// <summary>Creates an <see cref="T:Microsoft.AspNetCore.Mvc.Localization.IHtmlLocalizer" />.</summary>
        /// <param name="baseName"> The base name of the resource to load strings from. </param>
        /// <param name="location"> The location to load resources from. </param>
        /// <returns>The <see cref="T:Microsoft.AspNetCore.Mvc.Localization.IHtmlLocalizer" />.</returns>
        public IHtmlLocalizer Create(string baseName, string location)
        {
            return new DbHtmlLocalizer(baseName, location);
        }
    }
}