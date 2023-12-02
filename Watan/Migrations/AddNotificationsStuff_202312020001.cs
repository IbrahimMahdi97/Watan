using FluentMigrator;

namespace Watan.Migrations;

[Migration(202312020001)]
public class AddNotificationsStuff_202312020001: Migration
{
    public override void Up()
    {
        Alter.Table("Users").AddColumn("DeviceId").AsString(int.MaxValue).Nullable();
        
        Create.Table("Notifications")
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Title").AsString(250).NotNullable()
            .WithColumn("Body").AsString(int.MaxValue).NotNullable()
            .WithColumn("Type").AsInt32().NotNullable() //Enum for Notifications Types
            .WithColumn("ReferenceId").AsInt32().Nullable()
            .WithColumn("UserId").AsInt32().NotNullable()
            .ForeignKey("Users", "Id")
            .WithColumn("RecordDate").AsDateTime2().WithDefault(SystemMethods.CurrentDateTime);
    }

    public override void Down()
    {
        Alter.Column("DeviceId").OnTable("Users");
        Delete.Table("Notifications");
    }
}