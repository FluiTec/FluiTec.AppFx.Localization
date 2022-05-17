using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Events;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.TypeScanner
{
    /// <summary>
    ///     An attribute filtering type scanner.
    /// </summary>
    public abstract class AttributeFilteringTypeScanner : FilteringTypeScanner
    {
        /// <summary>
        ///     Specialized constructor for use only by derived class.
        /// </summary>
        /// <param name="helper">   The helper. </param>
        protected AttributeFilteringTypeScanner(ReflectionHelper helper) : base(helper)
        {
            TypeFilter = Filter;
        }

        /// <summary>
        ///     Gets the type filter.
        /// </summary>
        /// <value>
        ///     A function delegate that yields a bool.
        /// </value>
        public override Func<Type, bool> TypeFilter { get; }

        /// <summary>
        ///     Gets the filter attributes.
        /// </summary>
        /// <value>
        ///     The filter attributes.
        /// </value>
        public abstract IEnumerable<Type> FilterAttributes { get; }

        /// <summary>
        ///     Event queue for all listeners interested in TypeAttributeDetected events.
        /// </summary>
        protected event EventHandler<TypeAttributeDetectedEventArgs> TypeAttributeDetected;

        /// <summary>
        ///     Filters the given t.
        /// </summary>
        /// <param name="t">    A Type to process. </param>
        /// <returns>
        ///     True if it succeeds, false if it fails.
        /// </returns>
        protected virtual bool Filter(Type t)
        {
            return FilterAttributes.Any(a =>
            {
                var attributeData = t.GetCustomAttribute(a);
                var hasAttribute = attributeData != null;
                if (hasAttribute)
                    OnTypeAttributeDetected(t, a, attributeData);
                return hasAttribute;
            });
        }

        /// <summary>
        ///     Executes the 'type attribute detected' action.
        /// </summary>
        /// <param name="type">             The type. </param>
        /// <param name="attributeType">    Type of the attribute. </param>
        /// <param name="attribute">        The attribute. </param>
        protected virtual void OnTypeAttributeDetected(Type type, Type attributeType, Attribute attribute)
        {
            TypeAttributeDetected?.Invoke(this, new TypeAttributeDetectedEventArgs(type, attributeType, attribute));
        }
    }
}