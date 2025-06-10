using FluentMigrator;

namespace Watan.Migrations;

[Migration(202403100001)]
public class AddNewUserColumns_202403100001 : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AddColumn("InvitedByUsername").AsString(500).NotNullable()
            .AddColumn("VotingCenterName").AsString(500).NotNullable()
            .AddColumn("VotingCenterNumber").AsString(500).NotNullable()
            .AddColumn("PartnerName").AsString(500).NotNullable()
            .AddColumn("IsReceivingSocialSecurity").AsBoolean().Nullable();
    }

    public override void Down()
    {

    }
}