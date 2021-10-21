using System;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization
{
    /// <summary>
    /// A database string localizer factory.
    /// </summary>
    public class DbStringLocalizerFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </summary>
        ///
        /// <param name="baseName"> The base name of the resource to load strings from. </param>
        /// <param name="location"> The location to load resources from. </param>
        ///
        /// <returns>
        /// The <see cref="T:Microsoft.Extensions.Localization.IStringLocalizer" />.
        /// </returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}