using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Localization.Schema;

namespace FluiTec.AppFx.Localization.Entities
{
    /// <summary>
    /// A resource entity.
    /// </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.ResourceTable)]
    public class ResourceEntity : IKeyEntity<int>, IEquatable<ResourceEntity>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        ///
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        ///
        /// <value>
        /// The key.
        /// </value>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the modification date.
        /// </summary>
        ///
        /// <value>
        /// The modification date.
        /// </value>
        public DateTime ModificationDate { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the author.
        /// </summary>
        ///
        /// <value>
        /// The identifier of the author.
        /// </value>
        public int AuthorId { get; set; }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        ///
        /// <param name="other">    An object to compare with this object. </param>
        ///
        /// <returns>
        /// <see langword="true" /> if the current object is equal to the <paramref name="other" />
        /// parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(ResourceEntity other)
        {
            return Id.Equals(other?.Id) && 
                   Key.Equals(other?.Key) &&
                   ModificationDate.Equals(other?.ModificationDate) && 
                   AuthorId.Equals(other?.AuthorId);
        }
    }
}