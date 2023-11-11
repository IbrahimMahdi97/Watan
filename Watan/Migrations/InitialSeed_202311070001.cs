using System.Drawing;
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
                Description = "شكوى",
                Prefix= "CMP"
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
                MotherName = "الست الوالدة",
                ProvinceOfBirth = "بغداد / الكرخ",
                Gender = true,
                DateOfBirth = new DateTime(1998, 10, 15),
                PhoneNumber = "07733810890",
                ProvinceId = 3,
                TownId = 1,
                District = "123",
                StreetNumber = "12",
                HouseNumber = "1",
                NationalIdNumber = "123456789123456789",
                ResidenceCardNumber = "12345",
                VoterCardNumber = "1234567",
                Password = "58b5444cf1b6253a4317fe12daff411a78bda0a95279b1d5768ebf5ca60829e78da944e8a9160a0b6d428cb213e813525a72650dac67b88879394ff624da482f"
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
                Description = "شكوى",
                Prefix= "CMP"
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
                MotherName = "الست الوالدة",
                ProvinceOfBirth = "بغداد / الكرخ",
                Gender = true,
                DateOfBirth = new DateTime(1998, 10, 15),
                PhoneNumber = "07733810890",
                ProvinceId = 3,
                TownId = 1,
                District = "123",
                StreetNumber = "12",
                HouseNumber = "1",
                NationalIdNumber = "123456789123456789",
                ResidenceCardNumber = "12345",
                VoterCardNumber = "1234567",
                Password = "58b5444cf1b6253a4317fe12daff411a78bda0a95279b1d5768ebf5ca60829e78da944e8a9160a0b6d428cb213e813525a72650dac67b88879394ff624da482f"
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
