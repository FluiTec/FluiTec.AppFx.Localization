using FluiTec.DbLocalizationProvider;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.Cache;

namespace FluiTec.AppFx.Localization.Cache
{
    /// <summary>   A clear cache handler. </summary>
    public class ClearCacheHandler : ICommandHandler<ClearCache.Command>
    {
        /// <summary>
        ///     Handles the command. Actual instance of the command being executed is passed-in as
        ///     argument.
        /// </summary>
        /// <param name="command">  Actual command instance being executed. </param>
        public void Execute(ClearCache.Command command)
        {
            foreach (var itemToRemove in InMemoryCacheManager.Entries)
                ConfigurationContext.Current.CacheManager.Remove(itemToRemove.Key);
        }
    }
}