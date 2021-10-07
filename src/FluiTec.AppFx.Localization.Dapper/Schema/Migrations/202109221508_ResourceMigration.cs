﻿using FluiTec.AppFx.Data.Dapper.Migration;
using FluentMigrator;
using FluiTec.AppFx.Localization.Schema;
using FluiTec.AppFx.Localization.Entities;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migrations
{
    /// <summary>
    /// A resource entity.
    /// </summary>
    [DapperMigration(2021, 09, 22, 15, 08, "Achim Schnell")]
    public class ResourceMigration : Migration
    {
        /// <summary>
        /// (Immutable) the foreign key author.
        /// </summary>
        private const string ForeignKeyAuthor = "FK_Author_Resource";

        /// <summary>
        /// (Immutable) the unique resource key.
        /// </summary>
        private const string UniqueResourceKey = "UX_Resource_Key";

        /// <summary>
        /// Collect the UP migration expressions.
        /// </summary>
        public override void Up()
        {
            IfDatabase(MigrationDatabaseName.Mssql, MigrationDatabaseName.Pgsql)
                .Create
                .Table(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn(nameof(ResourceEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(ResourceEntity.Key)).AsString().NotNullable()
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
                .Column(nameof(ResourceEntity.Key));
            
            IfDatabase(MigrationDatabaseName.Mysql)
                .Create
                .Table(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn(nameof(ResourceEntity.Id)).AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn(nameof(ResourceEntity.Key)).AsString().NotNullable()
                .WithColumn(nameof(ResourceEntity.ModificationDate)).AsDateTimeOffset().NotNullable()
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
                .Table(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema);
            
            IfDatabase(MigrationDatabaseName.Mysql)
                .Delete
                .Table(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema);
        }
    }
}