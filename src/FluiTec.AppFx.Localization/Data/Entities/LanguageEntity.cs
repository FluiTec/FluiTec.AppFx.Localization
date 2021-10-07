using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Localization.Schema;

namespace FluiTec.AppFx.Localization.Entities
{
    /// <summary>
    ///     A language entity.
    /// </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.LanguageTable)]
    public class LanguageEntity : IKeyEntity<int>, IEquatable<LanguageEntity>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the name of the ISO.
        /// </summary>
        /// <value>
        ///     The name of the ISO.
        /// </value>
        public string IsoName { get; set; }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">    An object to compare with this object. </param>
        /// <returns>
        ///     <see langword="true" /> if the current object is equal to the <paramref name="other" />
        ///     parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(LanguageEntity other)
        {
            return Id.Equals(other?.Id) &&
                   Name.Equals(other?.Name) &&
                   IsoName.Equals(other?.IsoName);
        }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public int Id { get; set; }
    }
}