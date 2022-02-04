using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Attributes;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.TypeScanner
{
    /// <summary>
    /// A localized attribute filtering type scanner.
    /// </summary>
    public class LocalizedAttributeFilteringTypeScanner : AttributeFilteringTypeScanner
    {
        /// <summary>
        /// Gets the filter attributes.
        /// </summary>
        ///
        /// <value>
        /// The filter attributes.
        /// </value>
        public override IEnumerable<Type> FilterAttributes { get; } = new[] {typeof(LocalizedAttribute)};

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="helper">   The helper. </param>
        public LocalizedAttributeFilteringTypeScanner(ReflectionHelper helper) : base(helper) {}

        /// <summary>
        /// Gets the types in this collection.
        /// </summary>
        ///
        /// <param name="assembly"> The assembly. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the types in this collection.
        /// </returns>
        public override IEnumerable<Type> GetTypes(Assembly assembly)
        {
            var localizedTypes = base.GetTypes(assembly).ToList();
            Helper.LocalizedTypes.AddRange(localizedTypes);
            return localizedTypes;
        }

        /// <summary>
        /// Executes the 'type attribute detected' action.
        /// </summary>
        ///
        /// <param name="type">             The type. </param>
        /// <param name="attributeType">    Type of the attribute. </param>
        /// <param name="attribute">        The attribute. </param>
        protected override void OnTypeAttributeDetected(Type type, Type attributeType, Attribute attribute)
        {
            base.OnTypeAttributeDetected(type, attributeType, attribute);

            if (attributeType == typeof(LocalizedAttribute))
            {
                Helper.LocalizedTypeInfo.AddOrUpdate(type, 
                    tx => attribute as LocalizedAttribute, 
                    (ty, localizedAttribute) => attribute as LocalizedAttribute);
            }
        }
    }
}