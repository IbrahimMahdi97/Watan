namespace Repository.Query;

public static class UserQuery
{
    public const string CreateUserQuery =
        @"IF EXISTS(SELECT PhoneNumber FROM Users WHERE PhoneNumber = @PhoneNumber)
    SELECT 0;
ELSE
    INSERT INTO Users (FullName, MotherName, Gender, Email,  PhoneNumber, EmergencyPhoneNumber, ProvinceOfBirth, DateOfBirth,
                       Password, ProvinceId, TownId, District, StreetNumber, HouseNumber, NationalIdNumber, ResidenceCardNumber,
                       VoterCardNumber, Rating, MaritalStatus, JobPlace, RecruitmentYear, JobTitle, JobSector, JobType, GraduatedYear, GraduatedFromDepartment, GraduatedFromCollege, 
                       GraduatedFromUniversity, AcademicAchievement, StudyingYearsCount, JobDegree, FamilyMembersCount, ChildrenCount, JoiningDate, ClanName,
                       SubclanName, IsFamiliesOfMartyrs, MartyrRelationship, FinancialCondition)
    OUTPUT inserted.Id
    VALUES(@FullName, @MotherName, @Gender, @Email, @PhoneNumber, @EmergencyPhoneNumber, @ProvinceOfBirth, @DateOfBirth,
           @Password, @ProvinceId, @TownId, @District, @StreetNumber, @HouseNumber, @NationalIdNumber, @ResidenceCardNumber,
           @VoterCardNumber, @Rating, @MaritalStatus, @JobPlace, @RecruitmentYear, @JobTitle, @JobSector, @JobType, @GraduatedYear, @GraduatedFromDepartment, @GraduatedFromCollege,
           @GraduatedFromUniversity, @AcademicAchievement, @StudyingYearsCount, @JobDegree, @FamilyMembersCount, @ChildrenCount, @JoiningDate, @ClanName,
           @SubclanName, @IsFamiliesOfMartyrs, @MartyrRelationship, @FinancialCondition);";

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

    public const string SelectByParametersQuery = @"SELECT U.Id, U.FullName FROM Users U 
                                            JOIN UserRegions UR on U.Id = UR.UserId
                                            WHERE
                                              (UR.ProvinceId = 0 OR IIF(@ProvinceId = 0, 0, UR.ProvinceId) = @ProvinceId) AND
                                              (UR.TownId = 0 OR IIF(@TownId = 0, 0, UR.TownId) = @TownId) AND
                                              (UR.RegionId = 0 OR IIF(@RegionId = 0, 0, UR.RegionId) = @RegionId) AND
                                              (@SearchText IS NULL OR U.FullName LIKE '%' + @SearchText + '%'
                                                  OR U.MotherName LIKE '%' + @SearchText + '%'
                                                  OR U.ProvinceOfBirth LIKE '%' + @SearchText + '%'
                                                  OR U.PhoneNumber LIKE '%' + @SearchText + '%'
                                                  OR U.EmergencyPhoneNumber LIKE '%' + @SearchText + '%'
                                                  OR U.Email LIKE '%' + @SearchText + '%'
                                                  OR U.StreetNumber LIKE '%' + @SearchText + '%'
                                                  OR U.HouseNumber LIKE '%' + @SearchText + '%'
                                                  OR U.NationalIdNumber LIKE '%' + @SearchText + '%'
                                                  OR U.ResidenceCardNumber LIKE '%' + @SearchText + '%'
                                                  OR U.VoterCardNumber LIKE '%' + @SearchText + '%'
                                                  OR U.AcademicAchievement LIKE '%' + @SearchText + '%'
                                                  OR U.GraduatedFromUniversity LIKE '%' + @SearchText + '%'
                                                  OR U.GraduatedFromCollege LIKE '%' + @SearchText + '%'
                                                  OR U.GraduatedFromDepartment LIKE '%' + @SearchText + '%') AND
                                                (@Gender IS NULL OR U.Gender = @Gender) AND
                                                (@FromDateOfBirth IS NULL OR U.DateOfBirth >= @FromDateOfBirth) AND
                                                (@ToDateOfBirth IS NULL OR U.DateOfBirth <= @ToDateOfBirth) AND
                                                (@FromJoiningDate IS NULL OR U.JoiningDate >= @FromJoiningDate) AND
                                                (@ToJoiningDate IS NULL OR U.JoiningDate <= @ToJoiningDate) AND
                                              U.IsDeleted = 0
                                              ORDER BY U.RecordDate DESC
                                              OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY";

    public const string SelectCountByParametersQuery =
        @"SELECT COUNT( U.Id) FROM Users U 
                                            JOIN UserRegions UR on U.Id = UR.UserId
                                            WHERE
                                              (UR.ProvinceId = 0 OR IIF(@ProvinceId = 0, 0, UR.ProvinceId) = @ProvinceId) AND
                                              (UR.TownId = 0 OR IIF(@TownId = 0, 0, UR.TownId) = @TownId) AND
                                              (UR.RegionId = 0 OR IIF(@RegionId = 0, 0, UR.RegionId) = @RegionId) AND
                                              (@SearchText IS NULL OR U.FullName LIKE '%' + @SearchText + '%'
                                                  OR U.MotherName LIKE '%' + @SearchText + '%'
                                                  OR U.ProvinceOfBirth LIKE '%' + @SearchText + '%'
                                                  OR U.PhoneNumber LIKE '%' + @SearchText + '%'
                                                  OR U.EmergencyPhoneNumber LIKE '%' + @SearchText + '%'
                                                  OR U.Email LIKE '%' + @SearchText + '%'
                                                  OR U.StreetNumber LIKE '%' + @SearchText + '%'
                                                  OR U.HouseNumber LIKE '%' + @SearchText + '%'
                                                  OR U.NationalIdNumber LIKE '%' + @SearchText + '%'
                                                  OR U.ResidenceCardNumber LIKE '%' + @SearchText + '%'
                                                  OR U.VoterCardNumber LIKE '%' + @SearchText + '%'
                                                  OR U.AcademicAchievement LIKE '%' + @SearchText + '%'
                                                  OR U.GraduatedFromUniversity LIKE '%' + @SearchText + '%'
                                                  OR U.GraduatedFromCollege LIKE '%' + @SearchText + '%'
                                                  OR U.GraduatedFromDepartment LIKE '%' + @SearchText + '%') AND
                                                (@Gender IS NULL OR U.Gender = @Gender) AND
                                                (@FromDateOfBirth IS NULL OR U.DateOfBirth >= @FromDateOfBirth) AND
                                                (@ToDateOfBirth IS NULL OR U.DateOfBirth <= @ToDateOfBirth) AND
                                                (@FromJoiningDate IS NULL OR U.JoiningDate >= @FromJoiningDate) AND
                                                (@ToJoiningDate IS NULL OR U.JoiningDate <= @ToJoiningDate) AND
                                              U.IsDeleted = 0";

    public const string UpdateByIdQuery = @"UPDATE Users SET
                                                    FullName = @FullName,
                                                    MotherName = @MotherName,
                                                    ProvinceOfBirth = @ProvinceOfBirth,
                                                    Gender = @Gender,
                                                    DateOfBirth = @DateOfBirth,
                                                    District = @District,
                                                    StreetNumber = @StreetNumber,
                                                    HouseNumber = @HouseNumber,
                                                    NationalIdNumber = @NationalIdNumber,
                                                    ResidenceCardNumber = @ResidenceCardNumber,
                                                    VoterCardNumber = @VoterCardNumber,
                                                    PhoneNumber = @PhoneNumber,
                                                    EmergencyPhoneNumber = @EmergencyPhoneNumber,
                                                    Rating = @Rating,
                                                    MaritalStatus = @MaritalStatus,
                                                    JobPlace = @JobPlace,
                                                    RecruitmentYear = @RecruitmentYear,
                                                    JobTitle = @JobTitle,
                                                    JobSector = @JobSector,
                                                    JobType = @JobType,
                                                    GraduatedYear = @GraduatedYear,
                                                    GraduatedFromDepartment = @GraduatedFromDepartment,
                                                    GraduatedFromCollege = @GraduatedFromCollege,
                                                    GraduatedFromUniversity = @GraduatedFromUniversity,
                                                    AcademicAchievement = @AcademicAchievement,
                                                    StudyingYearsCount = @StudyingYearsCount,
                                                    JobDegree = @JobDegree,
                                                    FamilyMembersCount = @FamilyMembersCount,
                                                    ChildrenCount = @ChildrenCount,
                                                    JoiningDate = @JoiningDate,
                                                    ClanName = @ClanName,
                                                    SubClanName = @SubClanName,
                                                    IsFamiliesOfMartyrs = @IsFamiliesOfMartyrs,
                                                    MartyrRelationship = @MartyrRelationship,
                                                    Password = @Password,
                                                    FinancialCondition = @FinancialCondition
                                                    WHERE Id = @UserId
                                            ";

    public const string DeleteRolesByIdQuery = "DELETE FROM UserRoles WHERE  UserId = @UserId";
    public const string DeleteRegionsByIdQuery = "DELETE FROM UserRegions WHERE  UserId = @UserId";
}