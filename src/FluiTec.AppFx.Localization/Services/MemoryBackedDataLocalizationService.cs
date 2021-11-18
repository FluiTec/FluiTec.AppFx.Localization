using System;
using System.Collections.Generic;
using System.Globalization;
using FluiTec.AppFx.Localization.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.Services
{
    /// <summary>
    /// A service for accessing memory backed data localizations information.
    /// </summary>
    public class MemoryBackedDataLocalizationService : DataLocalizationService
    {
        #region Properties

        /// <summary>
        /// Gets the name of the source.
        /// </summary>
        ///
        /// <value>
        /// The name of the source.
        /// </value>
        protected override string SourceName { get; }

        /// <summary>
        /// Gets options for controlling the operation.
        /// </summary>
        ///
        /// <value>
        /// The options.
        /// </value>
        public ServiceLocalizationOptions Options { get; }

        /// <summary>
        /// Gets the cache.
        /// </summary>
        ///
        /// <value>
        /// The cache.
        /// </value>
        public IMemoryCache Cache { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="options">      Options for controlling the operation. </param>
        /// <param name="cache">        The cache. </param>
        /// <param name="dataService">  The data service. </param>
        public MemoryBackedDataLocalizationService(ServiceLocalizationOptions options, IMemoryCache cache, ILocalizationDataService dataService)
            : base(dataService)
        {
            Options = options ?? throw new ArgumentNullException(nameof(options));
            Cache = cache ?? throw new ArgumentNullException(nameof(cache));
            SourceName = $"MemoryCache/{dataService.Name}";
        }

        #endregion

        #region Methods

        /// <summary>
        /// By name.
        /// </summary>
        ///
        /// <param name="name">     The name. </param>
        /// <param name="culture">  The culture. </param>
        ///
        /// <returns>
        /// A LocalizedString.
        /// </returns>
        public override LocalizedString ByName(string name, CultureInfo culture)
        {
            return GetOrCreateWithPolicy(Cache, $"{name}+{culture.Name}", cacheEntry => base.ByName(name, culture), Options.MemoryCacheEntryOptions);
        }

        /// <summary>
        /// Enumerates by base name in this collection.
        /// </summary>
        ///
        /// <param name="baseName"> Name of the base. </param>
        /// <param name="culture">  The culture. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process by base name in this collection.
        /// </returns>
        public override IEnumerable<LocalizedString> ByBaseName(string baseName, CultureInfo culture)
        {
            return GetOrCreateWithPolicy(Cache, $"{baseName}+{culture.Name}", cacheEntry => base.ByBaseName(baseName, culture), Options.MemoryCacheEntryOptions);
        }

        /// <summary>
        /// Gets or create with policy.
        /// </summary>
        ///
        /// <typeparam name="TItem">    Type of the item. </typeparam>
        /// <param name="cache">        The cache. </param>
        /// <param name="key">          The key. </param>
        /// <param name="factory">      The factory. </param>
        /// <param name="entryOptions"> Options for controlling the entry. </param>
        ///
        /// <returns>
        /// The or create with policy.
        /// </returns>
        private static TItem GetOrCreateWithPolicy<TItem>(IMemoryCache cache, object key, Func<ICacheEntry, TItem> factory, MemoryCacheEntryOptions entryOptions)
        {
            if (cache.TryGetValue(key, out TItem result)) 
                return result;

            using var entry = cache
                .CreateEntry(key)
                .SetOptions(entryOptions);

            var fResult = factory(entry);
            entry.Value = fResult;

            return fResult;
        }

        #endregion
    }
}