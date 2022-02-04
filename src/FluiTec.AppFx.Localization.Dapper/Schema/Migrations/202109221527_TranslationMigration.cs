using FluiTec.AppFx.Data.Dapper.Migration;
using FluentMigrator;
using FluiTec.AppFx.Localization.Schema;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Data.Dapper.Extensions;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migrations
{
    /// <summary>
    /// A translation migration.
    /// </summary>
    [DapperMigration(2021, 09, 22, 15, 27, "Achim Schnell")]
    public class TranslationMigration : Migration
    {
        /// <summary>
        /// (Immutable) the foreign key resource.
        /// </summary>
        private const string ForeignKeyResource = "FK_Resource_Translation";

        /// <summary>
        /// (Immutable) the foreign key language.
        /// </summary>
        private const string ForeignKeyLanguage = "FK_Language_Translation";

        /// <summary>
        /// Collect the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.TranslationTable, true)
                .WithColumn(nameof(ResourceEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(TranslationEntity.ResourceId)).AsInt32().NotNullable()
                .WithColumn(nameof(TranslationEntity.LanguageId)).AsInt32().NotNullable()
                .WithColumn(nameof(TranslationEntity.Value)).AsString().Nullable();

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyResource)
                .FromTable(SchemaGlobals.TranslationTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(TranslationEntity.ResourceId))
                .ToTable(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn(nameof(ResourceEntity.Id));

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyLanguage)
                .FromTable(SchemaGlobals.TranslationTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(TranslationEntity.LanguageId))
                .ToTable(SchemaGlobals.LanguageTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn(nameof(LanguageEntity.Id));

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.TranslationTable, false)
                .WithColumn(nameof(ResourceEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(TranslationEntity.ResourceId)).AsInt32().NotNullable()
                .WithColumn(nameof(TranslationEntity.LanguageId)).AsInt32().NotNullable()
                .WithColumn(nameof(TranslationEntity.Value)).AsString().Nullable();
        }

        /// <summary>
        /// Collects the DOWN migration expressions.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyLanguage)
                .OnTable(SchemaGlobals.LanguageTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyResource)
                .OnTable(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema);
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.TranslationTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.TranslationTable, false);
        }
    }
}