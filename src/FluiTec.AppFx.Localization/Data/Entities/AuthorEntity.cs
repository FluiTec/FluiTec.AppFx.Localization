using System;
using FluiTec.AppFx.Data.Entities;

namespace FluiTec.AppFx.Localization.Entities
{
    /// <summary>
    /// An author entity.
    /// </summary>
    public class AuthorEntity : IKeyEntity<int>, IEquatable<AuthorEntity>
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
        /// Gets or sets the name.
        /// </summary>
        ///
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

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
        public bool Equals(AuthorEntity other)
        {
            return Id.Equals(other?.Id) &&
                   Name.Equals(other?.Name);
        }
    }
}