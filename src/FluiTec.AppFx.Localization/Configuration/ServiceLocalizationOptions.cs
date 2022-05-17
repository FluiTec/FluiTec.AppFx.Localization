using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluiTec.AppFx.Options.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Caching.Memory;

namespace FluiTec.AppFx.Localization.Configuration
{
    /// <summary>
    ///     A localization options.
    /// </summary>
    [ConfigurationKey("ServiceLocalizationOptions")]
    public class ServiceLocalizationOptions
    {
        /// <summary>
        ///     Default constructor.
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
            MissingLocalizationBehavior = MissingLocalizationBehavior.Throw;
        }

        /// <summary>
        ///     Gets or sets the default culture.
        /// </summary>
        /// <value>
        ///     The default culture.
        /// </value>
        public string DefaultCulture { get; set; }

        /// <summary>
        ///     Gets or sets the supported cultures.
        /// </summary>
        /// <value>
        ///     The supported cultures.
        /// </value>
        public List<string> SupportedCultures { get; set; }

        /// <summary>
        ///     Gets or sets options for controlling the memory cache entry.
        /// </summary>
        /// <value>
        ///     Options that control the memory cache entry.
        /// </value>
        public MemoryCacheEntryOptions MemoryCacheEntryOptions { get; set; }

        /// <summary>
        ///     Gets or sets the assembly filter exlusions.
        /// </summary>
        /// <value>
        ///     The assembly filter exlusions.
        /// </value>
        public List<string> AssemblyFilterExlusions { get; set; }

        /// <summary>
        ///     Gets or sets the missing localization behavior.
        /// </summary>
        /// <value>
        ///     The missing localization behavior.
        /// </value>
        public MissingLocalizationBehavior MissingLocalizationBehavior { get; set; }

        /// <summary>
        ///     Gets request localization options.
        /// </summary>
        /// <returns>
        ///     The request localization options.
        /// </returns>
        public RequestLocalizationOptions GetRequestLocalizationOptions()
        {
            return new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(DefaultCulture),
                SupportedCultures = new List<CultureInfo>(SupportedCultures.Select(c => new CultureInfo(c))),
                SupportedUICultures = new List<CultureInfo>(SupportedCultures.Select(c => new CultureInfo(c)))
            };
        }
    }

    /// <summary>
    ///     Values that represent missing localization behaviors.
    /// </summary>
    public enum MissingLocalizationBehavior
    {
        /// <summary>
        ///     An enum constant representing the ignore option.
        /// </summary>
        Ignore,

        /// <summary>
        ///     An enum constant representing the log option.
        /// </summary>
        Log,

        /// <summary>
        ///     An enum constant representing the throw option.
        /// </summary>
        Throw
    }
}