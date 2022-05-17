using FluentMigrator;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Localization.Schema;
using FluiTec.AppFx.Data.Dapper.Extensions;
using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migrations
{
    /// <summary>
    /// An author migration.
    /// </summary>
    [ExtendedMigrationAttribute(2021, 09, 22, 14, 27, "Achim Schnell")]
    public class AuthorMigration : Migration
    {
        private static readonly string UniqueNameConstraint = 
            UniqueIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.AuthorTable, nameof(AuthorEntity.Name));

        /// <summary>
        /// Collect the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.AuthorTable, true)
                .WithColumn(nameof(AuthorEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(AuthorEntity.Name)).AsString().NotNullable();

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .UniqueConstraint(UniqueNameConstraint)
                .OnTable(SchemaGlobals.AuthorTable)
                .WithSchema(SchemaGlobals.Schema)
                .Column(nameof(AuthorEntity.Name));

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.AuthorTable, false)
                .WithColumn(nameof(AuthorEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(AuthorEntity.Name)).AsString().NotNullable();
        }

        /// <summary>
        /// Collects the DOWN migration expressions.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .UniqueConstraint(UniqueNameConstraint)
                .FromTable(SchemaGlobals.AuthorTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.AuthorTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.AuthorTable, false);
        }
    }
}