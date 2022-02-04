using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Helpers;

namespace FluiTec.AppFx.Localization.Reflection.MemberScanner
{
    /// <summary>
    /// A filtering member scanner.
    /// </summary>
    public abstract class FilteringMemberScanner : DefaultMemberScanner
    {
        /// <summary>
        /// Gets the member filter.
        /// </summary>
        ///
        /// <value>
        /// A function delegate that yields a bool.
        /// </value>
        public abstract Func<Type, MemberInfo, bool> MemberFilter { get; }

        /// <summary>
        /// Specialized constructor for use only by derived class.
        /// </summary>
        ///
        /// <param name="helper">   The helper. </param>
        protected FilteringMemberScanner(ReflectionHelper helper) : base(helper) {}

        /// <summary>
        /// Gets the members in this collection.
        /// </summary>
        ///
        /// <param name="type"> The type. </param>
        ///
        /// <returns>
        /// An enumerator that allows foreach to be used to process the members in this collection.
        /// </returns>
        public override IEnumerable<MemberInfo> GetMembers(Type type)
        {
            return base.GetMembers(type).Where(m => MemberFilter(type, m));
        }
    }
}