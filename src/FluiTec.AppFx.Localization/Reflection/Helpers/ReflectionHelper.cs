using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using FluiTec.AppFx.Localization.Reflection.Attributes;

namespace FluiTec.AppFx.Localization.Reflection.Helpers
{
    /// <summary>
    /// A reflection helper.
    /// </summary>
    public class ReflectionHelper
    {
        #region Properties

        /// <summary>
        /// Gets a list of types of the distincts.
        /// </summary>
        ///
        /// <value>
        /// A list of types of the distincts.
        /// </value>
        public List<Type> DistinctTypes { get; }

        /// <summary>
        /// Gets a list of types of the localized.
        /// </summary>
        ///
        /// <value>
        /// A list of types of the localized.
        /// </value>
        public List<Type> LocalizedTypes { get; }

        /// <summary>
        /// Gets information describing the localized type.
        /// </summary>
        ///
        /// <value>
        /// Information describing the localized type.
        /// </value>
        public ConcurrentDictionary<Type, LocalizedAttribute> LocalizedTypeInfo { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ReflectionHelper()
        {
            DistinctTypes = new List<Type>();
            LocalizedTypes = new List<Type>();
            LocalizedTypeInfo = new ConcurrentDictionary<Type, LocalizedAttribute>();
        }

        #endregion

        #region Methods

        #endregion
    }
}