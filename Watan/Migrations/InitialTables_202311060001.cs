using FluentMigrator;

namespace Watan.Migrations
{
    [Migration(202311060001)]
    public class InitialTables_202311060001 : Migration
    {
        public override void Down()
        {
            Delete.Table("Users");
            Delete.Table("Roles");
            Delete.Table("Provinces");
            Delete.Table("Towns");
            Delete.Table("Regions");
            Delete.Table("UserRoles");
            Delete.Table("UserRegions");
            Delete.Table("Posts");
            Delete.Table("PostTypes");
            Delete.Table("EventDetails");
            Delete.Table("EventAttendance");
            Delete.Table("Notifications");
            Delete.Table("PostLikes");
            Delete.Table("PostComments");
            Delete.Table("CommentLikes");
            Delete.Table("CommentResponses");
            Delete.Table("Complaints");
            Delete.Table("ComplaintTypes");
        }

        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .ReferencedBy("Users","AddedByUserId")
                .WithColumn("FullName").AsString(250).NotNullable()
                .WithColumn("MotherName").AsString(250).NotNullable()
                .WithColumn("ProvinceOfBirth").AsString(50).NotNullable()
                .WithColumn("Gender").AsBoolean().NotNullable().WithDefaultValue(1)
                .WithColumn("DateOfBirth").AsDateTime2().NotNullable()
                .WithColumn("PhoneNumber").AsString(15).NotNullable()
                .WithColumn("EmergencyPhoneNumber").AsString(15).Nullable()
                .WithColumn("Email").AsString(250).Nullable()
                .WithColumn("XAccount").AsString(int.MaxValue).Nullable()
                .WithColumn("FacebookAccount").AsString(int.MaxValue).Nullable()
                .WithColumn("InstagramAccount").AsString(int.MaxValue).Nullable()
                .WithColumn("LinkedInAccount").AsString(int.MaxValue).Nullable()
                .WithColumn("ProvinceId").AsInt32().NotNullable()
                .WithColumn("TownId").AsInt32().NotNullable()
                .WithColumn("District").AsString(50).Nullable()
                .WithColumn("StreetNumber").AsString(50).Nullable()
                .WithColumn("HouseNumber").AsString(50).Nullable()
                .WithColumn("NationalIdNumber").AsString(50).NotNullable()
                .WithColumn("ResidenceCardNumber").AsString(50).NotNullable()
                .WithColumn("VoterCardNumber").AsString(50).NotNullable()
                
                .WithColumn("AcademicAchievement").AsString(50).NotNullable()
                .WithColumn("GraduatedFromUniversity").AsString(50).NotNullable()
                .WithColumn("GraduatedFromCollege").AsString(50).NotNullable()
                .WithColumn("GraduatedFromDepartment").AsString(50).NotNullable()
                .WithColumn("GraduatedYear").AsString(12).NotNullable()
                .WithColumn("StudyingYearsCount").AsInt32().NotNullable()
                .WithColumn("JobType").AsString(50).NotNullable()
                .WithColumn("JobSector").AsString(50).NotNullable()
                .WithColumn("JobTitle").AsString(50).NotNullable()
                .WithColumn("JobDegree").AsInt32().NotNullable()
                .WithColumn("RecruitmentYear").AsDateTime2().NotNullable()
                .WithColumn("JobPlace").AsString(150).NotNullable()
                .WithColumn("MaritalStatus").AsString(50).NotNullable()
                .WithColumn("FamilyMembersCount").AsInt32().NotNullable()
                .WithColumn("ChildrenCount").AsInt32().NotNullable()
                .WithColumn("JoiningDate").AsDateTime2().NotNullable()
                .WithColumn("ClanName").AsString(50).NotNullable()
                .WithColumn("SubClanName").AsString(50).NotNullable()
                .WithColumn("IsFamiliesOfMartyrs").AsBoolean().NotNullable()
                .WithColumn("MartyrRelationship").AsString(8).Nullable()
                .WithColumn("FinancialCondition").AsString(50).NotNullable()
                
                .WithColumn("Password").AsString(int.MaxValue).NotNullable()
                .WithColumn("RefreshToken").AsString().Nullable()
                .WithColumn("RefreshTokenExpiryTime").AsDateTime2().Nullable()
                .WithColumn("AddedByUserId").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(0)
                .WithColumn("RecordDate").AsDateTime2().WithDefault(SystemMethods.CurrentDateTime);

            Create.Table("Roles")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Description").AsString(50).NotNullable();
            
            Create.Table("Provinces")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Description").AsString(50).NotNullable();
            
            Create.Table("Towns")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Description").AsString(50).NotNullable()
                .WithColumn("ProvinceId").AsInt32().NotNullable()
                .ForeignKey("Provinces", "Id")
                .OnDelete(System.Data.Rule.None);
            
            Create.Table("Regions")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Description").AsString(50).NotNullable()
                .WithColumn("TownId").AsInt32().NotNullable()
                .ForeignKey("Towns", "Id")
                .OnDelete(System.Data.Rule.None);

            Create.Table("UserRoles")
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("RoleId").AsInt32().NotNullable()
                .ForeignKey("Roles", "Id")
                .OnDelete(System.Data.Rule.None);
            
            Create.Table("UserRegions")
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("ProvinceId").AsInt32().NotNullable().WithDefaultValue(0)  //Accepts 0 for all or FK From Provinces table
                .WithColumn("TownId").AsInt32().NotNullable().WithDefaultValue(0)  //Accepts 0 for all or FK From Towns table
                .WithColumn("RegionId").AsInt32().NotNullable().WithDefaultValue(0);  //Accepts 0 for all or FK From Regions table
            
            Create.Table("PostTypes")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Description").AsString(50).NotNullable()
                .WithColumn("Prefix").AsString(3).NotNullable();

            Create.Table("Posts")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Description").AsString(int.MaxValue).NotNullable()
                .WithColumn("TypeId").AsInt32().NotNullable()
                .ForeignKey("PostTypes", "Id")
                .OnDelete(System.Data.Rule.None)         
                .WithColumn("AddedByUserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None)         
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(0)
                .WithColumn("RecordDate").AsDateTime2().WithDefault(SystemMethods.CurrentDateTime);
            
            Create.Table("EventDetails")
                .WithColumn("PostId").AsInt32().NotNullable()
                .ForeignKey("Posts", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("Type").AsString(50).NotNullable()
                .WithColumn("ProvinceId").AsInt32().NotNullable()
                .ForeignKey("Provinces", "Id")
                .OnDelete(System.Data.Rule.None)      
                .WithColumn("TownId").AsInt32().NotNullable()
                .ForeignKey("Towns", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("Date").AsDateTime2().NotNullable()
                .WithColumn("StartTime").AsDateTime2().NotNullable()
                .WithColumn("EndTime").AsDateTime2().NotNullable()
                .WithColumn("LocationUrl").AsString(int.MaxValue).Nullable();
            
            Create.Table("EventAttendance")
                .WithColumn("PostId").AsInt32().NotNullable()
                .ForeignKey("Posts", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None);
            
            Create.Table("Notifications")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Title").AsString(50).NotNullable()
                .WithColumn("Description").AsString(int.MaxValue).NotNullable()
                .WithColumn("IsRead").AsBoolean().NotNullable().WithDefaultValue(0)
                .WithColumn("UserId").AsInt32().NotNullable() //Accepts 0 for all or FK From Users table
                .WithColumn("AddedByUserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None)         
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(0)
                .WithColumn("RecordDate").AsDateTime2().WithDefault(SystemMethods.CurrentDateTime);
            
            Create.Table("PostLikes")
                .WithColumn("PostId").AsInt32().NotNullable()
                .ForeignKey("Posts", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None);
            
            Create.Table("PostComments")   
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity().ReferencedBy("PostComments","ParentCommentId")
                .WithColumn("PostId").AsInt32().NotNullable()
                .ForeignKey("Posts", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("Comment").AsString(int.MaxValue).NotNullable()
                .WithColumn("ParentCommentId").AsInt32().Nullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(0)
                .WithColumn("RecordDate").AsDateTime2().WithDefault(SystemMethods.CurrentDateTime);
                
            Create.Table("CommentLikes")
                .WithColumn("CommentId").AsInt32().NotNullable()
                .ForeignKey("PostComments", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None);
            
            Create.Table("ComplaintTypes")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("Description").AsString(50).NotNullable()
                .WithColumn("Prefix").AsString(3).NotNullable();

            Create.Table("Complaints")
                .WithColumn("Id").AsInt32().NotNullable()
                .PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .ForeignKey("Users", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("TypeId").AsInt32().NotNullable()
                .ForeignKey("ComplaintTypes", "Id")
                .OnDelete(System.Data.Rule.None)
                .WithColumn("Details").AsString(int.MaxValue).NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable().WithDefaultValue(0)
                .WithColumn("RecordDate").AsDateTime2().WithDefault(SystemMethods.CurrentDateTime);
        }
    }
}
