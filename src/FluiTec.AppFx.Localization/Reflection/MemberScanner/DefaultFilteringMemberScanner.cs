using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Attributes;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.MemberScanner
{
    /// <summary>
    /// A default filtering member scanner.
    /// </summary>
    public class DefaultFilteringMemberScanner : FilteringMemberScanner
    {
        /// <summary>
        /// Gets the member filter.
        /// </summary>
        ///
        /// <value>
        /// A function delegate that yields a bool.
        /// </value>
        public override Func<Type, MemberInfo, bool> MemberFilter { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        ///
        /// <param name="helper">   The helper. </param>
        public DefaultFilteringMemberScanner(ReflectionHelper helper) : base(helper)
        {
            MemberFilter = FilterMember;
        }

        public override IEnumerable<MemberInfo> GetMembers(Type type)
        {
            if (!Helper.LocalizedTypeInfo.ContainsKey(type)) return base.GetMembers(type);

            var info = Helper.LocalizedTypeInfo[type];
            if (info.Inherited)
            {
                if (!info.OnlyIncluded)
                    return base.GetMembers(type);
                return base.GetMembers(type)
                    .Where(m => m.GetCustomAttribute<IncludeAttribute>() != null)
                    .Where(m => MemberFilter(type, m));
            }

            if (!info.OnlyIncluded)
                return type.GetMembers()
                    .Where(m => m.DeclaringType == type)
                    .Where(m => MemberFilter(type, m));
            return type.GetMembers()
                .Where(m => m.DeclaringType == type)
                .Where(m => m.GetCustomAttribute<IncludeAttribute>() != null)
                .Where(m => MemberFilter(type, m));

        }

        /// <summary>
        /// Filter member.
        /// </summary>
        ///
        /// <param name="type">     The type. </param>
        /// <param name="member">   The member. </param>
        ///
        /// <returns>
        /// True if it succeeds, false if it fails.
        /// </returns>
        protected virtual bool FilterMember(Type type, MemberInfo member)
        {
            if (type.IsClass) return FilterClassMember(type, member);
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (type.IsEnum) return FilterEnumMember(type, member);
            return false;
        }

        /// <summary>
        /// Filter enum member.
        /// </summary>
        ///
        /// <param name="type">     The type. </param>
        /// <param name="member">   The member. </param>
        ///
        /// <returns>
        /// True if it succeeds, false if it fails.
        /// </returns>
        protected virtual bool FilterEnumMember(Type type, MemberInfo member)
        {
            return (member.MemberType & MemberTypes.Field) != 0 && ((FieldInfo)member).IsStatic;
        }

        /// <summary>
        /// Filter class member.
        /// </summary>
        ///
        /// <param name="type">     The type. </param>
        /// <param name="member">   The member. </param>
        ///
        /// <returns>
        /// True if it succeeds, false if it fails.
        /// </returns>
        protected virtual bool FilterClassMember(Type type, MemberInfo member)
        {
            return (member.MemberType & MemberTypes.Property) != 0;
        }
    }
}