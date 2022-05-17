using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.TypeScanner
{
    /// <summary>
    ///     A filtering type scanner.
    /// </summary>
    public abstract class FilteringTypeScanner : DefaultTypeScanner
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="helper">   The helper. </param>
        protected FilteringTypeScanner(ReflectionHelper helper) : base(helper)
        {
        }

        /// <summary>
        ///     Gets the type filter.
        /// </summary>
        /// <value>
        ///     A function delegate that yields a bool.
        /// </value>
        public abstract Func<Type, bool> TypeFilter { get; }

        /// <summary>
        ///     Gets the types in this collection.
        /// </summary>
        /// <param name="assembly"> The assembly. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the types in this collection.
        /// </returns>
        public override IEnumerable<Type> GetTypes(Assembly assembly)
        {
            return base.GetTypes(assembly).Where(TypeFilter);
        }
    }
}