using System;
using System.Collections.Generic;
using System.Reflection;

namespace FluiTec.AppFx.Localization.Reflection.MemberScanner
{
    /// <summary>
    ///     Interface for member scanner.
    /// </summary>
    public interface IMemberScanner
    {
        /// <summary>
        ///     Gets the members in this collection.
        /// </summary>
        /// <param name="type"> The type. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the members in this collection.
        /// </returns>
        IEnumerable<MemberInfo> GetMembers(Type type);

        /// <summary>
        ///     Gets the members in this collection.
        /// </summary>
        /// <param name="types">    The types. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the members in this collection.
        /// </returns>
        IEnumerable<MemberInfo> GetMembers(IEnumerable<Type> types);
    }
}