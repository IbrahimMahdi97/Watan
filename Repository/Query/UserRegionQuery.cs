namespace Repository.Query;

public class UserRegionQuery
{
    public const string InsertUserRegionQuery = 
        @"INSERT INTO UserRegions (UserId, ProvinceId, TownId, RegionId)
              VALUES(@UserId, @ProvinceId, @TownId, @RegionId)";

    public const string UserRegionsByUserIdQuery = @"SELECT UserRegions.*, Provinces.*, Towns.*, Regions.* 
                                                    FROM UserRegions 
                                                    LEFT OUTER JOIN Provinces ON UserRegions.ProvinceId = Provinces.Id 
                                                    LEFT OUTER JOIN Towns ON UserRegions.TownId = Towns.Id 
                                                    LEFT OUTER JOIN Regions ON UserRegions.RegionId = Regions.Id 
                                                    WHERE UserRegions.UserId=@Id";
}