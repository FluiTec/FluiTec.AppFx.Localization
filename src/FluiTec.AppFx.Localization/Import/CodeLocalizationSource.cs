using System.Collections.Generic;
using System.Linq;
using FluiTec.AppFx.Localization.Configuration;
using FluiTec.AppFx.Localization.Reflection;
using FluiTec.AppFx.Localization.Reflection.AssemblyScanner;
using FluiTec.AppFx.Localization.Reflection.Attributes;
using FluiTec.AppFx.Localization.Reflection.MemberScanner;
using FluiTec.AppFx.Localization.Reflection.TypeScanner;

namespace FluiTec.AppFx.Localization.Import
{
    /// <summary>
    /// A code localization source.
    /// </summary>
    public class CodeLocalizationSource : BaseLocalizationSource
    {
        /// <summary>
        /// (Immutable) the code author.
        /// </summary>
        public const string CodeAuthor = "Import-Code";

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
        /// <param name="importOptions">    Options that control the import. </param>
        public CodeLocalizationSource(IAssemblyScanner assemblyScanner, ITypeScanner typeScanner,
            IMemberScanner memberScanner, ServiceLocalizationImportOptions importOptions) : base(importOptions)
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
        public override IEnumerable<ILocalizationResource> FindResources()
        {
            return 
                from assembly in AssemblyScanner.GetAssemblies() 
                from type in TypeScanner.GetTypes(assembly) 
                from member in MemberScanner.GetMembers(type) 
                from translationAttribute in member
                .GetCustomAttributes(typeof(TranslationAttribute), true)
                .Cast<TranslationAttribute>() select new LocalizationResource
            {
                ResourceKey = MemberInfoCollector.GetResourceKey(member),
                Author = CodeAuthor,
                Language = translationAttribute.LanguageIsoName,
                Translation = translationAttribute.Value
            };
        }
    }
}