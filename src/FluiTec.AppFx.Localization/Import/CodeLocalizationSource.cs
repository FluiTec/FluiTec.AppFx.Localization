using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.AssemblyScanner;
using FluiTec.AppFx.Localization.Reflection.MemberScanner;
using FluiTec.AppFx.Localization.Reflection.TypeScanner;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    /// A code localization source.
    /// </summary>
    public class CodeLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Gets the assembly scanner.
        /// </summary>
        ///
        /// <value>
        /// The assembly scanner.
        /// </value>
        public IAssemblyScanner AssemblyScanner { get; }

        /// <summary>
        /// Gets the type scanner.
        /// </summary>
        ///
        /// <value>
        /// The type scanner.
        /// </value>
        public ITypeScanner TypeScanner { get; }

        /// <summary>
        /// Gets the member scanner.
        /// </summary>
        ///
        /// <value>
        /// The member scanner.
        /// </value>
        public IMemberScanner MemberScanner { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="assemblyScanner">  The assembly scanner. </param>
        /// <param name="typeScanner">      The type scanner. </param>
        /// <param name="memberScanner">    The member scanner. </param>
        public CodeLocalizationSource(IAssemblyScanner assemblyScanner, ITypeScanner typeScanner,
            IMemberScanner memberScanner)
        {
            AssemblyScanner = assemblyScanner;
            TypeScanner = typeScanner;
            MemberScanner = memberScanner;
        }

        /// <summary>
        /// Finds the resources in this collection.
        /// </summary>
        /// <returns>
        ///
        /// An enumerator that allows foreach to be used to process the resources in this collection.
        /// </returns>
        public IEnumerable<ILocalizationResource> FindResources()
        {
            foreach (var a in AssemblyScanner.GetAssemblies())
            {
                foreach (var t in TypeScanner.GetTypes(a))
                {
                    foreach (var m in MemberScanner.GetMembers(t))
                    {
                        yield return new LocalizationResource
                        {
                            ResourceKey = MemberInfoCollector.GetResourceKey(m),
                            Author = "",
                            Language = "",
                            Translation = ""
                        };
                    }
                }
            }
        }
    }

    public static class MemberInfoCollector
    {
        /// <summary>
        /// Gets resource key.
        /// </summary>
        ///
        /// <param name="mi">   The mi. </param>
        ///
        /// <returns>
        /// The resource key.
        /// </returns>
        public static string GetResourceKey(MemberInfo mi)
        {
            var fullName = GetFullName(mi);
            return fullName;
        }

        /// <summary>
        /// Gets full name.
        /// </summary>
        ///
        /// <param name="mi">   The mi. </param>
        ///
        /// <returns>
        /// The full name.
        /// </returns>
        public static string GetFullName(MemberInfo mi)
        {
            var typeName = mi.DeclaringType?.FullName;
            return !string.IsNullOrWhiteSpace(typeName) ? $"{typeName}.{mi.Name}" : mi.Name;
        }
    }
}