using FluentMigrator;

namespace Watan.Migrations;

[Migration(202312020001)]
public class AddUserDeviceId_202312020001: Migration
{
    public override void Up()
    {
        Alter.Table("Users").AddColumn("DeviceId").AsString(int.MaxValue).Nullable();
    }

    public override void Down()
    {
        Alter.Column("DeviceId").OnTable("Users");
    }
}