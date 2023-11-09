namespace Repository.Query;

public static class UserQuery
{
    public const string CreateUserQuery =
        @"IF EXISTS(SELECT PhoneNumber FROM Users WHERE PhoneNumber = @PhoneNumber)
            SELECT 0;
            ELSE
            INSERT INTO Users (FullName, MotherName, Gender, Email,  PhoneNumber, ProvinceOfBirth, DateOfBirth, Password)
              OUTPUT inserted.Id
              VALUES(@FirstName, @MotherName, @Gender, @Email, @PhoneNumber, @ProvinceOfBirth, @DateOfBirth, @Password);";
    
    public const string AddEncryptedPasswordByIdQuery =
        @"UPDATE Users SET Password = @password WHERE Id = @id;";
    
    public const string UserRolesByUserIdQuery =
        @"SELECT Id, Description, UR.CityId, UR.MinistryId FROM Roles R
                       JOIN UserRoles UR on R.Id = UR.RoleId
                       WHERE UR.UserId = @Id";
    
    public const string UpdateRefreshTokenByIdQuery =
        @"UPDATE Users SET RefreshToken = @refreshToken, RefreshTokenExpiryTime = @refreshTokenExpiryTime WHERE Id = @id;";
    
    public const string UserIdByPhoneQuery =
        @"SELECT Id FROM Users WHERE Email = @phonenumber";
    
    public const string UserByIdQuery =
        @"SELECT * FROM Users WHERE Id = @id";
    
    public const string UserByCredentialsQuery =
        @"SELECT * FROM Users WHERE Email = @email AND Password = @Password";
}