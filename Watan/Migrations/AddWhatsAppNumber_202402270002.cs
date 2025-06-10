using FluentMigrator;

namespace Watan.Migrations;

[Migration(202402270002)]
public class AddWhatsAppNumber_202402270002 : Migration
{
    public override void Up()
    {
        Alter.Table("Users").AddColumn("WhatsAppNumber").AsString(15).NotNullable().WithDefaultValue("");
    }

    public override void Down()
    {
        Delete.Column("WhatsAppNumber").FromTable("Users");
    }
}