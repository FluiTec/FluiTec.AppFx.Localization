using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Localization;

namespace FluiTec.AppFx.Localization.Extensions
{
    /// <summary>
    /// A string localizer extension.
    /// </summary>
    public static class StringLocalizerExtension
    {
        /// <summary>
        /// An IStringLocalizer&lt;TResource&gt; extension method that localizes.
        /// </summary>
        ///
        /// <exception cref="ArgumentException">    Thrown when one or more arguments have unsupported or
        ///                                         illegal values. </exception>
        ///
        /// <typeparam name="TResource">    Type of the resource. </typeparam>
        /// <param name="localizer">    The localizer to act on. </param>
        /// <param name="localized">    The localized. </param>
        /// <param name="args">         A variable-length parameters list containing arguments. </param>
        ///
        /// <returns>
        /// A string.
        /// </returns>
        public static string Localize<TResource>(this IStringLocalizer<TResource> localizer,
            Expression<Func<TResource, object>> localized, params object[] args)
        {
            var member = localized.Body as MemberExpression;
            var property = member?.Member as PropertyInfo;
            if (property == null)
                throw new ArgumentException();
            return localizer[property.Name, args];
        }
    }
}