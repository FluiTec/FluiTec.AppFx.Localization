using System;

namespace FluiTec.AppFx.Localization.Reflection.Events
{
    /// <summary>
    ///     Additional information for type attribute detected events.
    /// </summary>
    public class TypeAttributeDetectedEventArgs : EventArgs
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="type">             The type. </param>
        /// <param name="attributeType">    Type of the attribute. </param>
        /// <param name="attribute">        The attribute. </param>
        public TypeAttributeDetectedEventArgs(Type type, Type attributeType, Attribute attribute)
        {
            Type = type;
            AttributeType = attributeType;
            Attribute = attribute;
        }

        /// <summary>
        ///     Gets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public Type Type { get; }

        /// <summary>
        ///     Gets the type of the attribute.
        /// </summary>
        /// <value>
        ///     The type of the attribute.
        /// </value>
        public Type AttributeType { get; }

        /// <summary>
        ///     Gets the attribute.
        /// </summary>
        /// <value>
        ///     The attribute.
        /// </value>
        public Attribute Attribute { get; }
    }
}