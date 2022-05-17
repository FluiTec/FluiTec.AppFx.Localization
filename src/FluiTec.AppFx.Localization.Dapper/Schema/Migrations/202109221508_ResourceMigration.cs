using FluiTec.AppFx.Data.Migration;
using FluiTec.AppFx.Data.Migration.NameGenerators;
using FluentMigrator;
using FluiTec.AppFx.Localization.Schema;
using FluiTec.AppFx.Localization.Entities;
using FluiTec.AppFx.Data.Dapper.Extensions;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migrations
{
    /// <summary>
    /// A resource entity.
    /// </summary>
    [ExtendedMigration(2021, 09, 22, 15, 08, "Achim Schnell")]
    public class ResourceMigration : Migration
    {
        /// <summary>
        /// (Immutable) the foreign key author.
        /// </summary>
        private static readonly string ForeignKeyAuthor =
            ForeignKeyIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.AuthorTable, SchemaGlobals.ResourceTable);

        /// <summary>
        /// (Immutable) the unique resource key.
        /// </summary>
        private static readonly string UniqueResourceKey = 
                UniqueIndexNameGenerator.CreateName(SchemaGlobals.Schema, SchemaGlobals.ResourceTable, nameof(ResourceEntity.ResourceKey));
        

        /// <summary>
        /// Collect the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.ResourceTable, true)
                .WithColumn(nameof(ResourceEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(ResourceEntity.ResourceKey)).AsString().NotNullable()
                .WithColumn(nameof(ResourceEntity.ModificationDate)).AsDateTimeOffset().NotNullable()
                .WithColumn(nameof(ResourceEntity.AuthorId)).AsInt32().NotNullable();

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .ForeignKey(ForeignKeyAuthor)
                .FromTable(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn(nameof(ResourceEntity.AuthorId))
                .ToTable(SchemaGlobals.AuthorTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn(nameof(AuthorEntity.Id));

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .UniqueConstraint(UniqueResourceKey)
                .OnTable(SchemaGlobals.ResourceTable)
                .WithSchema(SchemaGlobals.Schema)
                .Column(nameof(ResourceEntity.ResourceKey));
            
            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Create
                .Table(SchemaGlobals.Schema, SchemaGlobals.ResourceTable, false)
                .WithColumn(nameof(ResourceEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(ResourceEntity.ResourceKey)).AsString().NotNullable()
                .WithColumn(nameof(ResourceEntity.ModificationDate)).AsDateTime().NotNullable()
                .WithColumn(nameof(ResourceEntity.AuthorId)).AsInt32().NotNullable();
        }

        /// <summary>
        /// Collects the DOWN migration expressions.
        /// </summary>
        public override void Down()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .ForeignKey(ForeignKeyAuthor)
                .OnTable(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .UniqueConstraint(UniqueResourceKey)
                .FromTable(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema);
            
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.ResourceTable, true);
            
            IfDatabase(MigrationDatabaseName.Mysql, MigrationDatabaseName.Sqlite)
                .Delete
                .Table(SchemaGlobals.Schema, SchemaGlobals.ResourceTable, false);
        }
    }
}