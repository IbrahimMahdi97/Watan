﻿using FluentMigrator;

namespace Watan.Migrations;

[Migration(202312020002)]
public class AddUserRating_202312020002 : Migration
{
    public override void Up()
    {
        Alter.Table("Users").AddColumn("Rating").AsInt32().NotNullable().WithDefaultValue(0);
    }

    public override void Down()
    {
        Alter.Column("Rating").OnTable("Users");
    }
}