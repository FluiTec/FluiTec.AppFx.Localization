using System;

namespace FluiTec.AppFx.Localization.Reflection.Attributes
{
    /// <summary>
    ///     Attribute for include.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IncludeAttribute : Attribute
    {
    }
}