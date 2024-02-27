using FluentMigrator;

namespace Watan.Migrations;

[Migration(202402270001)]
public class AddUserChildrenTable_202402270001 : Migration
{
    public override void Up()
    {
        Create.Table("UserChildren")
            .WithColumn("UserId").AsInt32().NotNullable()
            .ForeignKey("Users", "Id")
            .OnDelete(System.Data.Rule.None)
            .WithColumn("ChildName").AsString(250).NotNullable()
            .WithColumn("Age").AsInt32().NotNullable();

    }

    public override void Down()
    {
        Delete.Table("UserChildren");
    }
}