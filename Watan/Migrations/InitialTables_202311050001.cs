using FluentMigrator;

namespace Watan.Migrations
{
    [Migration(202311050001)]
    public class InitialTables_202311050001 : Migration
    {
        public override void Down()
        {
            Delete.Table("Roles");
            Delete.Table("Users");
            Delete.Table("UserRoles");
        }

        public override void Up()
        {
           
            Create.Table("Roles")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Description").AsString(50).NotNullable();
            
            Create.Table("Users")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString(50).NotNullable()
                .WithColumn("RecordDate").AsDateTime2().WithDefault(SystemMethods.CurrentDateTime);

            Create.Table("UserRoles")
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("RoleId").AsInt32().NotNullable()
                .ForeignKey("Roles", "Id")
                .OnDelete(System.Data.Rule.None);
        }
    }
}
