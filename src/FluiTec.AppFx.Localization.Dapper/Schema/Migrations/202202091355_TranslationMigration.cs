using FluentMigrator;
using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Localization.Schema;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Data.Dapper.Extensions;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migrations
{
    /// <summary>
    /// A translation migration 2.
    /// </summary>
    [DapperMigration(2022, 02, 09, 13, 55, "Achim Schnell")]
    public class TranslationMigration2 : Migration
    {
        /// <summary>
        /// (Immutable) the foreign key author.
        /// </summary>
        private const string ForeignKeyAuthor = "FK_Author_Translation";

        /// <summary>
        /// Collect the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Alter
                .Table(SchemaGlobals.Schema, SchemaGlobals.TranslationTable, true)
                .AddColumn(nameof(TranslationEntity.ModificationDate)).AsDateTimeOffset().NotNullable()
                .AddColumn(nameof(TranslationEntity.AuthorId)).AsInt32().NotNullable();

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyAuthor)
                .FromTable(SchemaGlobals.TranslationTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(TranslationEntity.AuthorId))
                .ToTable(SchemaGlobals.AuthorTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn(nameof(AuthorEntity.Id));

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Alter
                .Table(SchemaGlobals.Schema, SchemaGlobals.TranslationTable, false)
                .AddColumn(nameof(TranslationEntity.ModificationDate)).AsDateTime().NotNullable()
                .AddColumn(nameof(TranslationEntity.AuthorId)).AsInt32().NotNullable();
        }

        /// <summary>
        /// Collects the DOWN migration expressions.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyAuthor)
                .OnTable(SchemaGlobals.TranslationTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Column(nameof(TranslationEntity.ModificationDate))
                .Column(nameof(TranslationEntity.AuthorId))
                .FromTable(SchemaGlobals.Schema, SchemaGlobals.TranslationTable, true);

            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Column(nameof(TranslationEntity.ModificationDate))
                .Column(nameof(TranslationEntity.AuthorId))
                .FromTable(SchemaGlobals.Schema, SchemaGlobals.TranslationTable, false);
        }
    }
}