namespace Repository.Query;

public static class UserQuery
{
    public const string CreateUserQuery =
        @"IF EXISTS(SELECT PhoneNumber FROM Users WHERE PhoneNumber = @PhoneNumber)
            SELECT 0;
            ELSE
            INSERT INTO Users (FullName, MotherName, Gender, Email,  PhoneNumber, EmergencyPhoneNumber, ProvinceOfBirth, DateOfBirth, Password, ProvinceId, TownId, District, StreetNumber, HouseNumber, NationalIdNumber, ResidenceCardNumber, VoterCardNumber)
              OUTPUT inserted.Id
              VALUES(@FullName, @MotherName, @Gender, @Email, @PhoneNumber, @EmergencyPhoneNumber, @ProvinceOfBirth, @DateOfBirth, @Password, @ProvinceId, @TownId, @District, @StreetNumber, @HouseNumber, @NationalIdNumber, @ResidenceCardNumber, @VoterCardNumber);";
    
    public const string AddEncryptedPasswordByIdQuery =
        @"UPDATE Users SET Password = @password WHERE Id = @id;";
    
    public const string UserRolesByUserIdQuery =
        @"SELECT Id, Description, UR.UserId FROM Roles R
                       JOIN UserRoles UR on R.Id = UR.RoleId
                       WHERE UR.UserId = @Id";
    
    public const string UpdateRefreshTokenByIdQuery =
        @"UPDATE Users SET RefreshToken = @refreshToken, RefreshTokenExpiryTime = @refreshTokenExpiryTime WHERE Id = @id;";
    
    public const string UserIdByEmailOrPhoneNumberQuery =
        @"SELECT Id FROM Users WHERE Email = @EmailOrPhoneNumber OR PhoneNumber = @EmailOrPhoneNumber ";
    
    public const string UserByIdQuery =
        @"SELECT * FROM Users WHERE Id = @id";
    
    public const string UpdateDeviceIdByIdQuery =
        @"UPDATE Users SET DeviceId = @deviceId WHERE Id = @id";
    
    public const string UpdateRatingByIdQuery =
        @"UPDATE Users SET Rating = @Rating WHERE Id = @UserId";
    
    public const string UserByCredentialsEmailOrPhoneNumberQuery = 
        @"SELECT * FROM Users WHERE ( Email = @EmailOrPhoneNumber OR PhoneNumber = @EmailOrPhoneNumber ) AND Password = @Password";
    
    public const string UserDeviceIdQuery = 
        @"SELECT DeviceId FROM Users WHERE Id = @id";

    public const string UsersByParametersQuery = @"SELECT US.Id AS Id, US.FullName AS FullName, 
                                                    US.MotherName AS MotherName, US.ProvinceOfBirth AS ProvinceOfBirth, 
                                                    US.Gender AS Gender, US.DateOfBirth AS DateOfBirth, 
                                                    US.District AS District, US.StreetNumber AS StreetNumber, 
                                                    US.HouseNumber AS HouseNumber, US.NationalIdNumber AS NationalIdNumber,
                                                    US.ResidenceCardNumber AS ResidenceCardNumber, 
                                                    US.VoterCardNumber AS VoterCardNumber,
                                                    US.PhoneNumber AS PhoneNumber, US.Email AS Email, US.Rating AS Rating 
                                                    FROM Users US INNER JOIN UserRoles UR ON US.Id = UR.UserId 
                                                    INNER JOIN UserRegions UG ON US.Id = UG.UserId 
                                                    WHERE IIF(@ProvinceId = 0, 0, UG.ProvinceId) = @ProvinceId AND
                                                    IIF(@TownId = 0, 0, UG.TownId) = @TownId AND 
                                                    IIF(@RegionId = 0, 0, UG.RegionId) = @RegionId AND 
                                                    IIF(@RoleId = 0, 0, UR.RoleId) = @RoleId AND 
                                                    IIF(@Id = 0, 0, US.Id) = @Id AND US.IsDeleted = 0 
                                                    ORDER BY US.RecordDate DESC
                                                    OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY";
    public const string UsersCountByParametersQuery = @"SELECT COUNT(US.Id) 
                                                        FROM Users US INNER JOIN UserRoles UR ON US.Id = UR.UserId 
                                                        INNER JOIN UserRegions UG ON US.Id = UG.UserId 
                                                        WHERE IIF(@ProvinceId = 0, 0, UG.ProvinceId) = @ProvinceId AND
                                                        IIF(@TownId = 0, 0, UG.TownId) = @TownId AND 
                                                        IIF(@RegionId = 0, 0, UG.RegionId) = @RegionId AND 
                                                        IIF(@RoleId = 0, 0, UR.RoleId) = @RoleId AND 
                                                        IIF(@Id = 0, 0, US.Id) = @Id AND US.IsDeleted = 0 ";
}