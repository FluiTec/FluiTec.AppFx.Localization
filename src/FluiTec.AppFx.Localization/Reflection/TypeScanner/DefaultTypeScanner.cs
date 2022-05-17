using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.TypeScanner
{
    /// <summary>
    ///     A default type scanner.
    /// </summary>
    public class DefaultTypeScanner : ITypeScanner
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="helper">   The helper. </param>
        public DefaultTypeScanner(ReflectionHelper helper)
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
        ///     Gets the types in this collection.
        /// </summary>
        /// <param name="assembly"> The assembly. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the types in this collection.
        /// </returns>
        public virtual IEnumerable<Type> GetTypes(Assembly assembly)
        {
            var types = assembly.GetTypes().Distinct().ToList();
            Helper.DistinctTypes.AddRange(types);
            return types;
        }

        /// <summary>
        ///     Gets the types in this collection.
        /// </summary>
        /// <param name="assemblies">   The assemblies. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the types in this collection.
        /// </returns>
        public virtual IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies)
        {
            var types = new List<Type>();
            foreach (var a in assemblies)
                types.AddRange(GetTypes(a));
            return types.Distinct();
        }
    }
}