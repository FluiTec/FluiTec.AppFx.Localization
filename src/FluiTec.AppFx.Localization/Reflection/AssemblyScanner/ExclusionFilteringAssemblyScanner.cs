using System;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.AssemblyScanner
{
    /// <summary>
    ///     An exclusion filtering assembly scanner.
    /// </summary>
    public class ExclusionFilteringAssemblyScanner : FilteringAssemblyScanner
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="options">  Options for controlling the operation. </param>
        /// <param name="helper">   The helper. </param>
        public ExclusionFilteringAssemblyScanner(ServiceLocalizationOptions options, ReflectionHelper helper) :
            base(helper)
        {
            Options = options;
            AssemblyFilter = a => !Options.AssemblyFilterExlusions
                .Any(e => a.FullName.StartsWith(e));
        }

        /// <summary>
        ///     Gets options for controlling the operation.
        /// </summary>
        /// <value>
        ///     The options.
        /// </value>
        public ServiceLocalizationOptions Options { get; }

        /// <summary>
        ///     Gets the assembly filter.
        /// </summary>
        /// <value>
        ///     A function delegate that yields a bool.
        /// </value>
        public override Func<Assembly, bool> AssemblyFilter { get; }
    }
}