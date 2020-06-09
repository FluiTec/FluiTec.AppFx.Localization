using System;
using FluiTec.AppFx.Data.Entities;
using FluiTec.AppFx.Data.EntityNameServices;

namespace FluiTec.AppFx.Localization.Entities
{
    /// <summary>
    ///     A resource entity.
    /// </summary>
    [EntityName("AppFxLocalization","Resource")]
    public class ResourceEntity : IKeyEntity<int>
    {
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        public string ResourceKey { get; set; }

        /// <summary>   Gets or sets the modification date. </summary>
        /// <value> The modification date. </value>
        public DateTime ModificationDate { get; set; }

        /// <summary>   Gets or sets the author. </summary>
        /// <value> The author. </value>
        public string Author { get; set; }

        /// <summary>   Gets or sets a value indicating whether from code. </summary>
        /// <value> True if from code, false if not. </value>
        public bool FromCode { get; set; }

        /// <summary>   Gets or sets the is modified. </summary>
        /// <value> The is modified. </value>
        public bool? IsModified { get; set; }

        /// <summary>   Gets or sets the is hidden. </summary>
        /// <value> The is hidden. </value>
        public bool? IsHidden { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
    }
}