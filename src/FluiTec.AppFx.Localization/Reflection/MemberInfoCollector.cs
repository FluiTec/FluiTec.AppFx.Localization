using System.Reflection;

namespace FluiTec.AppFx.Localization.Reflection
{
    /// <summary>
    /// A member information collector.
    /// </summary>
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

        /// <summary>
        /// Information about the display translation attribute.
        /// </summary>
        public class DisplayTranslationAttributeInfo
        {
            public string Translation { get; set; }

            public string Language { get; set; }
        }
    }
}