﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.DbLocalizationProvider.Abstractions;
using FluiTec.DbLocalizationProvider.Internal;

namespace FluiTec.DbLocalizationProvider.Sync.Collectors
{
    internal class UseResourceAttributeCollector : IResourceCollector
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
            // try to understand if there is resource "redirect" - [UseResource(..)]
            var resourceRef = mi.GetCustomAttribute<UseResourceAttribute>();
            if (resourceRef != null)
                TypeDiscoveryHelper.UseResourceAttributeCache.TryAdd(resourceKey,
                    ResourceKeyBuilder.BuildResourceKey(resourceRef.TargetContainer, resourceRef.PropertyName));

            return Enumerable.Empty<DiscoveredResource>();
        }
    }
}