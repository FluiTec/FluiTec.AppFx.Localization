using FluentMigrator.Runner.VersionTableInfo;
using FluiTec.AppFx.Localization.Schema;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migration
{
    /// <summary>	A version table. </summary>
    [VersionTableMetaData]
    public class VersionTable : IVersionTableMetaData
    {
        /// <summary>	Name of the column. </summary>
        public string ColumnName => "Version";

        /// <summary>	True to owns schema. </summary>
        public bool OwnsSchema => true;

        /// <summary>	Name of the schema. </summary>
        public string SchemaName => SchemaGlobals.Schema;

        /// <summary>	Name of the table. </summary>
        public string TableName => "AppFxLocalization_Version";

        /// <summary>	Name of the unique index. </summary>
        public string UniqueIndexName => "UC_Version";

        /// <summary>	Name of the applied on column. </summary>
        public virtual string AppliedOnColumnName => "AppliedOn";

        /// <summary>	Name of the description column. </summary>
        public virtual string DescriptionColumnName => "Description";

        /// <summary>	Gets or sets a context for the application. </summary>
        /// <value>	The application context. </value>
        public object ApplicationContext { get; set; }
    }
}