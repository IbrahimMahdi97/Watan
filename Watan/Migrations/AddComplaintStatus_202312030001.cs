using FluentMigrator;

namespace Watan.Migrations;

[Migration(202312030001)]
public class AddComplaintStatus_202312030001 : Migration
{
    public override void Up()
    {
        Alter.Table("Complaints").AddColumn("Status").AsInt32().NotNullable().WithDefaultValue(0);
    }

    public override void Down()
    {
        Alter.Column("Status").OnTable("Complaints");
    }
}