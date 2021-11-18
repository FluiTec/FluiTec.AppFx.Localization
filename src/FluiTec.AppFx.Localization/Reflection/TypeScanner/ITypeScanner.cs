using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluiTec.AppFx.Localization.Reflection.TypeScanner
{
    /// <summary>
    /// Interface for type scanner.
    /// </summary>
    public interface ITypeScanner
    {
        /// <summary>
        /// Gets the types in this collection.
        /// </summary>
        ///
        /// <param name="assembly"> The assembly. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the types in this collection.
        /// </returns>
        IEnumerable<Type> GetTypes(Assembly assembly);

        /// <summary>
        /// Gets the types in this collection.
        /// </summary>
        ///
        /// <param name="assemblies">   The assemblies. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the types in this collection.
        /// </returns>
        IEnumerable<Type> GetTypes(IEnumerable<Assembly> assemblies);
    }
}