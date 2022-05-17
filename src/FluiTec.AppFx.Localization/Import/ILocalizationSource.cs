using System.Collections.Generic;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    ///     Interface for localization source.
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        ///     Finds the resources in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        IEnumerable<ILocalizationResource> FindResources();
    }
}