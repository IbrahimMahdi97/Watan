﻿using System.Drawing;
using FluentMigrator;

namespace Watan.Migrations
{
    [Migration(202311070001)]
    public class InitialSeed_202311070001 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("PostTypes").Row(new {
                Description = "خبر",
                Prefix= "NWS"
            }).Row(new {
                Description = "حدث",
                Prefix= "EVT"
            }); 
            
            Delete.FromTable("ComplaintTypes").Row(new {
                Description = "طلب",
                Prefix= "REQ"
            }).Row(new {
                Description = "اقتراح",
                Prefix= "SUG"
            }).Row(new {
                Description = "مظلومية",
                Prefix= "UNF"
            }).Row(new {
                Description = "تعديل بيانات",
                Prefix= "CNG"
            });    
            
            Delete.FromTable("Roles").Row(new {
                Description = "member"
            }).Row(new {
                Description = "manager"
            }).Row(new {
                Description = "admin"
            });
            
            Delete.FromTable("Provinces").Row(new {
                Description = "الأنبار"
            }).Row(new {
               Description = "بابل"
            }).Row(new {
                Description = "بغداد"
            }).Row(new {
                Description = "البصرة"
            }).Row(new {
                Description = "القادسية"
            }).Row(new {
                Description = "ديالى"
            }).Row(new {
                Description = "ذي قار"
            }).Row(new {
                Description = "صلاح الدين"
            }).Row(new {
                Description = "كركوك"
            }).Row(new {
                Description = "كربلاء"
            }).Row(new {
                Description = "المثنى"
            }).Row(new {
                Description = "ميسان"
            }).Row(new {
                Description = "النجف"
            }).Row(new {
                Description = "نينوى"
            }).Row(new {
                Description = "واسط"
            });
            
            Delete.FromTable("Towns").Row(new {
                Description = "المنصور",
                ProvinceId = 3
            });    
            
            Insert.IntoTable("Regions").Row(new {
                Description = "حي دراغ",
                TownId = 1
            });
            
            Delete.FromTable("Users").Row(new {
                FullName = "رئيس الحزب",
                MotherName = "اسم الام",
                ProvinceOfBirth = "بغداد / الكرخ",
                Gender = true,
                DateOfBirth = new DateTime(1998, 10, 15),
                PhoneNumber = "07712345678",
                ProvinceId = 3,
                TownId = 1,
                District = "123",
                Email="tadmin@tadmin.com",
                StreetNumber = "12",
                HouseNumber = "1",
                NationalIdNumber = "123456789123456789",
                ResidenceCardNumber = "12345",
                VoterCardNumber = "1234567",
                Password = "b6b329ed260338df68892554b8efcf8dbc18933035aa57c40308e4008f0cdeaad9570eec79c5d48e06edc1af07ea212223e33d75ef61af4594e6e669c4b3aeb6",
                AcademicAchievement ="بكالوريوس",
                GraduatedFromUniversity ="جامعة بغداد",
                GraduatedFromCollege ="كلية الهندسة",
                GraduatedFromDepartment ="هندسة حاسبات",
                GraduatedYear ="2000 - 2001",
                StudyingYearsCount =4,
                JobType ="مكتبي",
                JobSector ="قطاع خاص",
                JobTitle ="مبرمج",
                JobDegree =0,
                RecruitmentYear =new DateTime(2021, 01, 01),
                JobPlace ="الكرادة - العرصات",
                MaritalStatus ="أعزب",
                FamilyMembersCount =7,
                ChildrenCount =0,
                JoiningDate =new DateTime(2022, 11, 01),
                ClanName ="سوداني",
                SubclanName ="بو العزيز",
                IsFamiliesOfMartyrs =false,
                FinancialCondition ="جيدة"
                });
            
            Delete.FromTable("UserRoles").Row(new {
                UserId = 1,
                RoleId = 1,
            });
            
            Delete.FromTable("UserRegions").Row(new {
                UserId = 1,
                ProvinceId = 0,
                TownId = 0,
                RegionId = 0
            });
        }

        public override void Up()
        {
            Insert.IntoTable("PostTypes").Row(new {
                Description = "خبر",
                Prefix= "NWS"
            }).Row(new {
                Description = "حدث",
                Prefix= "EVT"
            }); 
            
            Insert.IntoTable("ComplaintTypes").Row(new {
                Description = "طلب",
                Prefix= "REQ"
            }).Row(new {
                Description = "اقتراح",
                Prefix= "SUG"
            }).Row(new {
                Description = "مظلومية",
                Prefix= "UNF"
            }).Row(new {
                Description = "تعديل بيانات",
                Prefix= "CNG"
            });    
            
            Insert.IntoTable("Roles").Row(new {
                Description = "admin"
            }).Row(new {
                Description = "manager"
            }).Row(new {
                Description = "member"
            });
            
            Insert.IntoTable("Provinces").Row(new {
                Description = "الأنبار"
            }).Row(new {
                Description = "بابل"
            }).Row(new {
                Description = "بغداد"
            }).Row(new {
                Description = "البصرة"
            }).Row(new {
                Description = "القادسية"
            }).Row(new {
                Description = "ديالى"
            }).Row(new {
                Description = "ذي قار"
            }).Row(new {
                Description = "صلاح الدين"
            }).Row(new {
                Description = "كركوك"
            }).Row(new {
                Description = "كربلاء"
            }).Row(new {
                Description = "المثنى"
            }).Row(new {
                Description = "ميسان"
            }).Row(new {
                Description = "النجف"
            }).Row(new {
                Description = "نينوى"
            }).Row(new {
                Description = "واسط"
            });
            
            Insert.IntoTable("Towns").Row(new {
                Description = "المنصور",
                ProvinceId = 3
            });    
            
            Insert.IntoTable("Regions").Row(new {
                Description = "حي دراغ",
                TownId = 1
            });
            
            Insert.IntoTable("Users").Row(new {
                FullName = "رئيس الحزب",
                MotherName = "اسم الام",
                ProvinceOfBirth = "بغداد / الكرخ",
                Gender = true,
                DateOfBirth = new DateTime(1998, 10, 15),
                PhoneNumber = "07712345678",
                ProvinceId = 3,
                TownId = 1,
                District = "123",
                Email="tadmin@tadmin.com",
                StreetNumber = "12",
                HouseNumber = "1",
                NationalIdNumber = "123456789123456789",
                ResidenceCardNumber = "12345",
                VoterCardNumber = "1234567",
                Password = "b6b329ed260338df68892554b8efcf8dbc18933035aa57c40308e4008f0cdeaad9570eec79c5d48e06edc1af07ea212223e33d75ef61af4594e6e669c4b3aeb6",
                AcademicAchievement ="بكالوريوس",
                GraduatedFromUniversity ="جامعة بغداد",
                GraduatedFromCollege ="كلية الهندسة",
                GraduatedFromDepartment ="هندسة حاسبات",
                GraduatedYear ="2000 - 2001",
                StudyingYearsCount =4,
                JobType ="مكتبي",
                JobSector ="قطاع خاص",
                JobTitle ="مبرمج",
                JobDegree =0,
                RecruitmentYear =new DateTime(2021, 01, 01),
                JobPlace ="الكرادة - العرصات",
                MaritalStatus ="أعزب",
                FamilyMembersCount =7,
                ChildrenCount =0,
                JoiningDate =new DateTime(2022, 11, 01),
                ClanName ="سوداني",
                SubclanName ="بو العزيز",
                IsFamiliesOfMartyrs =false,
                FinancialCondition ="جيدة"
            });

            Insert.IntoTable("UserRoles").Row(new {
                UserId = 1,
                RoleId = 1,
            });        
            
            Insert.IntoTable("UserRegions").Row(new {
                UserId = 1,
                ProvinceId = 0,
                TownId = 0,
                RegionId = 0
            });

        }
    }
}
