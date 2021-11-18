using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace FluiTec.AppFx.Localization.Configuration
{
    /// <summary>
    /// A localization options.
    /// </summary>
    public class ServiceLocalizationOptions
    {
        /// <summary>
        /// Gets or sets options for controlling the memory cache entry.
        /// </summary>
        ///
        /// <value>
        /// Options that control the memory cache entry.
        /// </value>
        public MemoryCacheEntryOptions MemoryCacheEntryOptions { get; set; }

        /// <summary>
        /// Gets or sets the assembly filter exlusions.
        /// </summary>
        ///
        /// <value>
        /// The assembly filter exlusions.
        /// </value>
        public List<string> AssemblyFilterExlusions { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ServiceLocalizationOptions()
        {
            MemoryCacheEntryOptions = new MemoryCacheEntryOptions();
            AssemblyFilterExlusions = new List<string>
            {
                "Microsoft", 
                "mscorlib", 
                "System", 
                "EntityFramework", 
                "Newtonsoft", 
                "netstandard"
            };
        }
    }
}