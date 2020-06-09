using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.Internal;
using FluiTec.DbLocalizationProvider.Refactoring;

namespace FluiTec.DbLocalizationProvider.Sync.Collectors
{
    internal class ValidationAttributeCollector : IResourceCollector
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
            var keyAttributes = mi.GetCustomAttributes<ResourceKeyAttribute>().ToList();
            var validationAttributes = mi.GetCustomAttributes<ValidationAttribute>().ToList();

            if(keyAttributes.Count > 1 && validationAttributes.Any())
                throw new InvalidOperationException(
                    "Model with data annotation attributes cannot have more than one `[ResourceKey]` attribute.");

            foreach(var validationAttribute in validationAttributes)
            {
                if(validationAttribute.GetType() == typeof(DataTypeAttribute))
                    continue;

                if(keyAttributes.Any())
                    resourceKey = keyAttributes.First().Key;

                var validationResourceKey = ResourceKeyBuilder.BuildResourceKey(resourceKey, validationAttribute);
                var propertyName = validationResourceKey.Split('.').Last();

                var oldResourceKeys = OldResourceKeyBuilder.GenerateOldResourceKey(target, propertyName, mi,
                    resourceKeyPrefix, typeOldName, typeOldNamespace);

                var translations = new List<DiscoveredTranslation>();
                translations.AddRange(DiscoveredTranslation.FromSingle(
                    string.IsNullOrEmpty(validationAttribute.ErrorMessage)
                        ? propertyName
                        : validationAttribute.ErrorMessage));

                var validationTranslations = mi.GetCustomAttributes<ValidationTranslationForCultureAttribute>();
                foreach(var validationTranslationAttribute in validationTranslations)
                {
                    var validationAttributeName = validationAttribute.GetType().Name;
                    if(validationAttributeName.EndsWith("Attribute"))
                        validationAttributeName =
                            validationAttributeName.Substring(0,
                                validationAttributeName.LastIndexOf("Attribute", StringComparison.Ordinal));
                    if(validationTranslationAttribute.Validation == validationAttributeName)
                    {
                        translations.Add(new DiscoveredTranslation(validationTranslationAttribute.Translation,
                            validationTranslationAttribute.Culture));
                    }
                }

                yield return new DiscoveredResource(mi,
                    validationResourceKey,
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
}
