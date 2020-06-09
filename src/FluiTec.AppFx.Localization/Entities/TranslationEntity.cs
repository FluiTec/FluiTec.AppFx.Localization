using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;

namespace FluiTec.AppFx.Localization.Entities
{
    /// <summary>
    ///     A translation.
    /// </summary>
    [EntityName("AppFxLocalization","Translation")]
    public class TranslationEntity : IKeyEntity<int>
    {
        /// <summary>Gets or sets the language.</summary>
        /// <value>The language.</value>
        public string Language { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>Gets or sets the identifier of the resource.</summary>
        /// <value>The identifier of the resource.</value>
        public int ResourceId { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
    }
}