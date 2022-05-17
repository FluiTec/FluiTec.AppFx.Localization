using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.AssemblyScanner
{
    /// <summary>
    ///     A filtering assembly scanner.
    /// </summary>
    public abstract class FilteringAssemblyScanner : DefaultAssemblyScanner
    {
        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="helper">   The helper. </param>
        protected FilteringAssemblyScanner(ReflectionHelper helper) : base(helper)
        {
        }

        /// <summary>
        ///     Gets the assembly filter.
        /// </summary>
        /// <value>
        ///     A function delegate that yields a bool.
        /// </value>
        public abstract Func<Assembly, bool> AssemblyFilter { get; }

        /// <summary>
        ///     Gets the assemblies in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the assemblies in this collection.
        /// </returns>
        public override IEnumerable<Assembly> GetAssemblies()
        {
            return base.GetAssemblies().Where(AssemblyFilter);
        }
    }
}