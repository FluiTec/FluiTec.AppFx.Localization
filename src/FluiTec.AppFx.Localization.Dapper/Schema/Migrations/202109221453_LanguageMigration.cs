using FluentMigrator;
using FluiTec.AppFx.Localization.Schema;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migrations
{
    /// <summary>
    /// A language migration.
    /// </summary>
    [ExtendedMigration(2021, 09, 22, 14, 53, "Achim Schnell")]
    public class LanguageMigration : Migration
    {
        /// <summary>
        /// (Immutable) the unique name constraint.
        /// </summary>
        private static readonly string UniqueNameConstraint =
            UniqueIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.LanguageTable, nameof(LanguageEntity.Name));

        /// <summary>
        /// (Immutable) the unique ISO name constraint.
        /// </summary>
        private static readonly string UniqueIsoNameConstraint = 
                UniqueIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.LanguageTable, nameof(LanguageEntity.IsoName));

        /// <summary>
        /// Collect the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.LanguageTable, true)
                .WithColumn(nameof(LanguageEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(LanguageEntity.Name)).AsString().NotNullable()
                .WithColumn(nameof(LanguageEntity.IsoName)).AsString().NotNullable();

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .UniqueConstraint(UniqueNameConstraint)
                .OnTable(SchemaGlobals.LanguageTable)
                .WithSchema(SchemaGlobals.Schema)
                .Column(nameof(LanguageEntity.Name));

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .UniqueConstraint(UniqueIsoNameConstraint)
                .OnTable(SchemaGlobals.LanguageTable)
                .WithSchema(SchemaGlobals.Schema)
                .Column(nameof(LanguageEntity.IsoName));

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.LanguageTable, false)
                .WithColumn(nameof(LanguageEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(LanguageEntity.Name)).AsString().NotNullable()
                .WithColumn(nameof(LanguageEntity.IsoName)).AsString().NotNullable();
        }

        /// <summary>
        /// Collects the DOWN migration expressions.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .UniqueConstraint(UniqueIsoNameConstraint)
                .FromTable(SchemaGlobals.LanguageTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .UniqueConstraint(UniqueNameConstraint)
                .FromTable(SchemaGlobals.LanguageTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.LanguageTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.LanguageTable, false);
        }
    }
}