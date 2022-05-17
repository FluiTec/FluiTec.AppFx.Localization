using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.AssemblyScanner
{
    /// <summary>
    ///     A default assembly scanner.
    /// </summary>
    public class DefaultAssemblyScanner : IAssemblyScanner
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="helper">   The helper. </param>
        public DefaultAssemblyScanner(ReflectionHelper helper)
        {
            Helper = helper;
        }

        /// <summary>
        ///     Gets the helper.
        /// </summary>
        /// <value>
        ///     The helper.
        /// </value>
        public ReflectionHelper Helper { get; }

        /// <summary>
        ///     Gets the assemblies in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the assemblies in this collection.
        /// </returns>
        public virtual IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies().Distinct();
        }
    }
}