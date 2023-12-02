namespace Repository.Query;

public static class UserQuery
{
    public const string CreateUserQuery =
        @"IF EXISTS(SELECT PhoneNumber FROM Users WHERE PhoneNumber = @PhoneNumber)
            SELECT 0;
            ELSE
            INSERT INTO Users (FullName, MotherName, Gender, Email,  PhoneNumber, ProvinceOfBirth, DateOfBirth, Password, ProvinceId, TownId, District, StreetNumber, HouseNumber, NationalIdNumber, ResidenceCardNumber, VoterCardNumber)
              OUTPUT inserted.Id
              VALUES(@FullName, @MotherName, @Gender, @Email, @PhoneNumber, @ProvinceOfBirth, @DateOfBirth, @Password, @ProvinceId, @TownId, @District, @StreetNumber, @HouseNumber, @NationalIdNumber, @ResidenceCardNumber, @VoterCardNumber);";
    
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
}