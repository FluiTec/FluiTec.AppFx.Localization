using System;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Localization.Entities
{
    /// <summary>
    /// A resource entity.
    /// </summary>
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
        /// Gets or sets the resource key.
        /// </summary>
        ///
        /// <value>
        /// The resource key.
        /// </value>
        public string ResourceKey { get; set; }

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
                   ResourceKey.Equals(other?.ResourceKey) &&
                   ModificationDate.Equals(other?.ModificationDate) && 
                   AuthorId.Equals(other?.AuthorId);
        }
    }
}