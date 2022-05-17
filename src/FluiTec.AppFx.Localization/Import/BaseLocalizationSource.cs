using System.Collections.Generic;
using FluiTec.AppFx.Localization.Configuration;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    ///     A base localization source.
    /// </summary>
    public abstract class BaseLocalizationSource : ILocalizationSource
    {
        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="importOptions">    Options for controlling the import. </param>
        protected BaseLocalizationSource(ServiceLocalizationImportOptions importOptions)
        {
            ImportOptions = importOptions;
        }

        /// <summary>
        ///     Gets options for controlling the import.
        /// </summary>
        /// <value>
        ///     Options that control the import.
        /// </value>
        public ServiceLocalizationImportOptions ImportOptions { get; }

        /// <summary>
        ///     Finds the resources in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public abstract IEnumerable<ILocalizationResource> FindResources();
    }
}