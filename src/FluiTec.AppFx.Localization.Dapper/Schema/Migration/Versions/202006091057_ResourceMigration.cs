using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Localization.Schema;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migration.Versions
{
    /// <summary>   A resource migration. </summary>
    [DapperMigration(2020,06,09,10,57, "Achim Schnell")]
    public class _202006091057_ResourceMigration : FluentMigrator.Migration
    {
        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ResourceKey").AsString().NotNullable().Unique()
                .WithColumn("Author").AsString().NotNullable()
                .WithColumn("FromCode").AsBoolean().NotNullable()
                .WithColumn("IsModified").AsBoolean().Nullable()
                .WithColumn("ModificationDate").AsDateTime().NotNullable()
                .WithColumn("IsHidden").AsBoolean().NotNullable();

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.ResourceTable}")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ResourceKey").AsString().NotNullable().Unique()
                .WithColumn("Author").AsString().NotNullable()
                .WithColumn("FromCode").AsBoolean().NotNullable()
                .WithColumn("IsModified").AsBoolean().Nullable()
                .WithColumn("ModificationDate").AsDateTime().NotNullable()
                .WithColumn("IsHidden").AsBoolean().NotNullable();
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.ResourceTable}");
        }
    }
}
