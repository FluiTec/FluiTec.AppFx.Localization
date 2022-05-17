using System.Collections.Generic;
using System.Reflection;

namespace FluiTec.AppFx.Localization.Reflection.AssemblyScanner
{
    /// <summary>
    ///     Interface for assembly scanner.
    /// </summary>
    public interface IAssemblyScanner
    {
        /// <summary>
        ///     Gets the assemblies in this collection.
        /// </summary>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the assemblies in this collection.
        /// </returns>
        IEnumerable<Assembly> GetAssemblies();
    }
}