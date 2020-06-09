using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.Refactoring;

namespace FluiTec.DbLocalizationProvider.Sync.Collectors
{
    internal class DisplayAttributeCollector : IResourceCollector
    {
        public IEnumerable<DiscoveredResource> GetDiscoveredResources(
            Type target,
            object instance,
            MemberInfo mi,
            string translation,
            string resourceKey,
            string resourceKeyPrefix,
            bool typeKeyPrefixSpecified,
            bool isHidden,
            string typeOldName,
            string typeOldNamespace,
            Type declaringType,
            Type returnType,
            bool isSimpleType)
        {
            // try to fetch also [Display()] attribute to generate new "...-Description" resource => usually used for help text labels
            var displayAttribute = mi.GetCustomAttribute<DisplayAttribute>();
            if(displayAttribute?.Description == null) yield break;
            var propertyName = $"{mi.Name}-Description";
            var oldResourceKeys = OldResourceKeyBuilder.GenerateOldResourceKey(target, propertyName, mi,
                resourceKeyPrefix, typeOldName, typeOldNamespace);

            var translations = new List<DiscoveredTranslation>();
            translations.AddRange(DiscoveredTranslation.FromSingle(displayAttribute.Description));
            var validationTranslations = mi.GetCustomAttributes<DisplayTranslationForCultureAttribute>();
            foreach(var validationTranslationAttribute in validationTranslations)
            {
                var validationAttributeName = displayAttribute.GetType().Name;
                if(validationAttributeName.EndsWith("Attribute"))
                    validationAttributeName =
                        validationAttributeName.Substring(0,
                            validationAttributeName.LastIndexOf("Attribute", StringComparison.Ordinal));
                translations.Add(new DiscoveredTranslation(validationTranslationAttribute.Translation,
                    validationTranslationAttribute.Culture));
            }

            yield return new DiscoveredResource(mi,
                $"{resourceKey}",
                translations,
                propertyName,
                declaringType,
                returnType,
                isSimpleType)
            {
                TypeName = target.Name,
                TypeNamespace = target.Namespace,
                TypeOldName = oldResourceKeys.Item2,
                TypeOldNamespace = typeOldNamespace,
                OldResourceKey = oldResourceKeys.Item1
            };
        }
    }
}
