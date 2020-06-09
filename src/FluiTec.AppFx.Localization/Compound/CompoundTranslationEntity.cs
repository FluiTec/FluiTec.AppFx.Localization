using System.Collections.Generic;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Compound
{
    /// <summary>   A compound translation entity. </summary>
    public class CompoundTranslationEntity
    {
        #region Constructors

        /// <summary>   Default constructor. </summary>
        public CompoundTranslationEntity()
        {
            Translations = new List<TranslationEntity>();
        }

        #endregion

        #region Properties

        /// <summary>   Gets or sets the resource. </summary>
        /// <value> The resource. </value>
        public ResourceEntity Resource { get; set; }

        /// <summary>   Gets or sets the translations. </summary>
        /// <value> The translations. </value>
        public List<TranslationEntity> Translations { get; set; }

        #endregion
    }
}