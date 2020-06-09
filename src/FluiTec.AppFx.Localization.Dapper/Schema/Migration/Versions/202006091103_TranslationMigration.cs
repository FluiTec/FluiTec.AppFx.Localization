using FluiTec.AppFx.Data.Dapper.Migration;
using FluiTec.AppFx.Localization.Schema;

namespace FluiTec.AppFx.Localization.Dapper.Schema.Migration.Versions
{
    /// <summary>   A translation migration. </summary>
    [DapperMigration(2020,06,09,11,03, "Achim Schnell")]
    public class _202006091103_TranslationMigration : FluentMigrator.Migration
    {
        /// <summary>   Name of the foreign key. </summary>
        private const string ForeignKeyName = "FK_Translation_Resource";

        /// <summary>Name of the contraint.</summary>
        private const string ContraintName = "UX_Language_Resource";

        /// <summary>   Collect the UP migration expressions. </summary>
        public override void Up()
        {
            IfDatabase("sqlserver", "postgres")
                .Create
                .Table(SchemaGlobals.TranslationTable)
                .InSchema(SchemaGlobals.Schema)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ResourceId").AsInt32().NotNullable()
                .WithColumn("Value").AsString(int.MaxValue).Nullable()
                .WithColumn("Language").AsString().Nullable();

            IfDatabase("sqlserver", "postgres")
                .Create
                .ForeignKey(ForeignKeyName)
                .FromTable(SchemaGlobals.TranslationTable)
                .InSchema(SchemaGlobals.Schema)
                .ForeignColumn("ResourceId")
                .ToTable(SchemaGlobals.ResourceTable)
                .InSchema(SchemaGlobals.Schema)
                .PrimaryColumn("Id");

            IfDatabase("sqlserver", "postgres")
                .Create
                .UniqueConstraint(ContraintName)
                .OnTable(SchemaGlobals.TranslationTable)
                .WithSchema(SchemaGlobals.Schema)
                .Columns("ResourceId", "Language");

            IfDatabase("mysql")
                .Create
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.TranslationTable}")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("ResourceId").AsInt32().NotNullable()
                .WithColumn("Value").AsString(int.MaxValue).Nullable()
                .WithColumn("Language").AsString().Nullable();

            IfDatabase("mysql")
                .Create
                .UniqueConstraint(ContraintName)
                .OnTable($"{SchemaGlobals.Schema}_{SchemaGlobals.TranslationTable}")
                .Columns("ResourceId", "Language");
        }

        /// <summary>   Collects the DOWN migration expressions. </summary>
        public override void Down()
        {
            IfDatabase("sqlserver", "postgres")
                .Delete
                .ForeignKey(ForeignKeyName)
                .OnTable(SchemaGlobals.TranslationTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("sqlserver", "postgres")
                .Delete
                .Table(SchemaGlobals.TranslationTable)
                .InSchema(SchemaGlobals.Schema);

            IfDatabase("sqlserver", "postgres")
                .Delete
                .UniqueConstraint(ContraintName)
                .FromTable($"{SchemaGlobals.Schema}_{SchemaGlobals.TranslationTable}");

            IfDatabase("mysql")
                .Delete
                .Table($"{SchemaGlobals.Schema}_{SchemaGlobals.TranslationTable}");

            IfDatabase("mysql")
                .Alter
                .Column("Value")
                .OnTable($"{SchemaGlobals.Schema}_{SchemaGlobals.TranslationTable}")
                .AsString();
        }
    }
}
