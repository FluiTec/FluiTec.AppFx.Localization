using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;
using FluiTec.AppFx.Data.Extensions;
using FluiTec.AppFx.Localization.Schema;

namespace FluiTec.AppFx.Localization.Entities
{
    /// <summary>
    ///     A translation entity.
    /// </summary>
    [EntityName(SchemaGlobals.Schema, SchemaGlobals.TranslationTable)]
    public class TranslationEntity : IKeyEntity<int>, IEquatable<TranslationEntity>
    {
        /// <summary>
        ///     Gets or sets the identifier of the resource.
        /// </summary>
        /// <value>
        ///     The identifier of the resource.
        /// </value>
        public int ResourceId { get; set; }

        /// <summary>
        ///     Gets or sets the identifier of the language.
        /// </summary>
        /// <value>
        ///     The identifier of the language.
        /// </value>
        public int LanguageId { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        ///     Gets or sets the modification date.
        /// </summary>
        /// <value>
        ///     The modification date.
        /// </value>
        public DateTimeOffset ModificationDate { get; set; }

        /// <summary>
        ///     Gets or sets the identifier of the author.
        /// </summary>
        /// <value>
        ///     The identifier of the author.
        /// </value>
        public int AuthorId { get; set; }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">    An object to compare with this object. </param>
        /// <returns>
        ///     <see langword="true" /> if the current object is equal to the <paramref name="other" />
        ///     parameter; otherwise, <see langword="false" />.
        /// </returns>
        public bool Equals(TranslationEntity other)
        {
            return Id.Equals(other?.Id) &&
                   ResourceId.Equals(other?.ResourceId) &&
                   LanguageId.Equals(other?.LanguageId) &&
                   ModificationDate.EqualsRemovingPrecision(other?.ModificationDate) &&
                   AuthorId.Equals(other?.AuthorId) &&
                   Value.Equals(other?.Value);
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