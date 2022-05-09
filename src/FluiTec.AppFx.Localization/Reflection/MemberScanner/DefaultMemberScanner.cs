using System;
using System.Collections.Generic;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.MemberScanner
{
    /// <summary>
    /// A default member scanner.
    /// </summary>
    public class DefaultMemberScanner : IMemberScanner
    {
        /// <summary>
        /// Gets the helper.
        /// </summary>
        ///
        /// <value>
        /// The helper.
        /// </value>
        public ReflectionHelper Helper { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        ///
        /// <param name="helper">   The helper. </param>
        public DefaultMemberScanner(ReflectionHelper helper)
        {
            Helper = helper;
        }

        /// <summary>
        /// Gets the members in this collection.
        /// </summary>
        ///
        /// <param name="type"> The type. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the members in this collection.
        /// </returns>
        public virtual IEnumerable<MemberInfo> GetMembers(Type type)
        {
            return type.GetMembers();
        }

        /// <summary>
        /// Gets the members in this collection.
        /// </summary>
        ///
        /// <param name="types">    The types. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the members in this collection.
        /// </returns>
        public virtual IEnumerable<MemberInfo> GetMembers(IEnumerable<Type> types)
        {
            var members = new List<MemberInfo>();
            foreach (var t in types)
                members.AddRange(GetMembers(t));
            return members;
        }
    }
}