using FluiTec.AppFx.Data.Dapper.Migration;
using FluentMigrator;
using FluiTec.AppFx.Localization.Schema;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migrations
{
    /// <summary>
    /// A language migration.
    /// </summary>
    [DapperMigration(2021, 09, 22, 14, 53, "Achim Schnell")]
    public class LanguageMigration : Migration
    {
        /// <summary>
        /// (Immutable) the unique name constraint.
        /// </summary>
        private const string UniqueNameConstraint = "UX_Language_Name";

        /// <summary>
        /// (Immutable) the unique ISO name constraint.
        /// </summary>
        private const string UniqueIsoNameConstraint = "UX_Language_IsoName";

        /// <summary>
        /// Collect the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.LanguageTable)
                .InSchema(SchemaGlobals.Schema)
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

            IfDatabase(MigrationDatabaseName.Mysql)
                .Create
                .Table(SchemaGlobals.LanguageTable)
                .InSchema(SchemaGlobals.Schema)
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
                .Table(SchemaGlobals.LanguageTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mysql)
                .Delete
                .Table(SchemaGlobals.LanguageTable)
                .InSchema(SchemaGlobals.Schema);
        }
    }
}