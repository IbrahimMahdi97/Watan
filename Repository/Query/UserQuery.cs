namespace Repository.Query;

public static class UserQuery
{
    public const string CreateUserQuery =
        @"IF EXISTS(SELECT PhoneNumber FROM Users WHERE PhoneNumber = @PhoneNumber)
    SELECT 0;
ELSE
    INSERT INTO Users (FullName, MotherName, Gender, Email,  PhoneNumber, WhatsAppNumber, EmergencyPhoneNumber, ProvinceOfBirth, DateOfBirth,
                       Password, ProvinceId, TownId, District, StreetNumber, HouseNumber, NationalIdNumber, ResidenceCardNumber,
                       VoterCardNumber, Rating, MaritalStatus, JobPlace, RecruitmentYear, JobTitle, JobSector, JobType, GraduatedYear, GraduatedFromDepartment, GraduatedFromCollege, 
                       GraduatedFromUniversity, AcademicAchievement, StudyingYearsCount, JobDegree, FamilyMembersCount, ChildrenCount, JoiningDate, ClanName,
                       SubclanName, IsFamiliesOfMartyrs, MartyrRelationship, FinancialCondition)
    OUTPUT inserted.Id
    VALUES(@FullName, @MotherName, @Gender, @Email, @PhoneNumber, @WhatsAppNumber, @EmergencyPhoneNumber, @ProvinceOfBirth, @DateOfBirth,
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
                                              
                                              (@StudyingYearsCount IS NULL OR U.StudyingYearsCount = @StudyingYearsCount) AND
                                              (@JobDegree IS NULL OR U.JobDegree = @JobDegree) AND
                                              (@FamilyMembersCount IS NULL OR U.FamilyMembersCount = @FamilyMembersCount) AND
                                              (@ChildrenCount IS NULL OR U.ChildrenCount = @ChildrenCount) AND
                                              
                                              (@FullName IS NULL OR U.FullName LIKE '%' + @FullName + '%') AND
                                              (@MotherName IS NULL OR U.MotherName LIKE '%' + @MotherName + '%') AND
                                              (@ProvinceOfBirth IS NULL OR U.ProvinceOfBirth LIKE '%' + @ProvinceOfBirth + '%') AND
                                              (@PhoneNumber IS NULL OR U.PhoneNumber LIKE '%' + @PhoneNumber + '%') AND
                                              (@WhatsAppNumber IS NULL OR U.WhatsAppNumber LIKE '%' + @WhatsAppNumber + '%') AND
                                              (@EmergencyPhoneNumber IS NULL OR U.EmergencyPhoneNumber LIKE '%' + @EmergencyPhoneNumber + '%') AND
                                              (@Email IS NULL OR U.Email LIKE '%' + @Email + '%') AND
                                              (@District IS NULL OR U.District LIKE '%' + @District + '%') AND
                                              (@StreetNumber IS NULL OR U.StreetNumber LIKE '%' + @StreetNumber + '%') AND
                                              (@HouseNumber IS NULL OR U.HouseNumber LIKE '%' + @HouseNumber + '%') AND
                                              (@NationalIdNumber IS NULL OR U.NationalIdNumber LIKE '%' + @NationalIdNumber + '%') AND
                                              (@ResidenceCardNumber IS NULL OR U.ResidenceCardNumber LIKE '%' + @ResidenceCardNumber + '%') AND
                                              (@VoterCardNumber IS NULL OR U.VoterCardNumber LIKE '%' + @VoterCardNumber + '%') AND
                                              (@AcademicAchievement IS NULL OR U.AcademicAchievement LIKE '%' + @AcademicAchievement + '%') AND
                                              (@GraduatedFromUniversity IS NULL OR U.GraduatedFromUniversity LIKE '%' + @GraduatedFromUniversity + '%') AND
                                              (@GraduatedFromCollege IS NULL OR U.GraduatedFromCollege LIKE '%' + @GraduatedFromCollege + '%') AND
                                              (@GraduatedFromDepartment IS NULL OR U.GraduatedFromDepartment LIKE '%' + @GraduatedFromDepartment + '%') AND
                                              (@GraduatedYear IS NULL OR U.GraduatedYear LIKE '%' + @GraduatedYear + '%') AND
                                              (@JobType IS NULL OR U.JobType LIKE '%' + @JobType + '%') AND
                                              (@JobSector IS NULL OR U.JobSector LIKE '%' + @JobSector + '%') AND
                                              (@JobTitle IS NULL OR U.JobTitle LIKE '%' + @JobTitle + '%') AND
                                              (@JobPlace IS NULL OR U.JobPlace LIKE '%' + @JobPlace + '%') AND
                                              (@MaritalStatus IS NULL OR U.MaritalStatus LIKE '%' + @MaritalStatus + '%') AND
                                              (@ClanName IS NULL OR U.ClanName LIKE '%' + @ClanName + '%') AND
                                              (@SubClanName IS NULL OR U.SubClanName LIKE '%' + @SubClanName + '%') AND
                                              (@MartyrRelationship IS NULL OR U.MartyrRelationship LIKE '%' + @MartyrRelationship + '%') AND
                                              (@FinancialCondition IS NULL OR U.FinancialCondition LIKE '%' + @FinancialCondition + '%') AND

                                                (@Gender IS NULL OR U.Gender = @Gender) AND
                                                (@IsFamiliesOfMartyrs IS NULL OR U.IsFamiliesOfMartyrs = @IsFamiliesOfMartyrs) AND
                                                
                                                (@FromDateOfBirth IS NULL OR U.DateOfBirth >= @FromDateOfBirth) AND
                                                (@ToDateOfBirth IS NULL OR U.DateOfBirth <= @ToDateOfBirth) AND
                                                (@FromJoiningDate IS NULL OR U.JoiningDate >= @FromJoiningDate) AND
                                                (@ToJoiningDate IS NULL OR U.JoiningDate <= @ToJoiningDate) AND
                                                (@FromRecruitmentYear IS NULL OR U.RecruitmentYear >= @FromRecruitmentYear) AND
                                                (@ToRecruitmentYear IS NULL OR U.RecruitmentYear <= @ToRecruitmentYear) AND
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
                                                    WhatsAppNumber = @WhatsAppNumber,
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

    public const string SelectCountByProvinceIdAndTownIdQuery = """
                                                                SELECT COUNT(Id) FROM Users
                                                                    WHERE IIF(@ProvinceId = 0, 0, ProvinceId) = @ProvinceId
                                                                    AND IIF(@TownId = 0, 0, TownId) = @TownId
                                                                """;

    public const string SelectCountFromDateToDateQuery = """
                                                         WITH UserCounts AS (
                                                             SELECT
                                                                 COUNT(*) AS TotalCount,
                                                                 COUNT(CASE WHEN JoiningDate BETWEEN @FromDate AND @ToDate THEN 1 END) AS NewCount
                                                             FROM
                                                                 Users
                                                         ),
                                                         ActiveCounts AS (
                                                             SELECT
                                                                 COUNT(DISTINCT U.Id) AS ActiveCount
                                                             FROM
                                                                 Users U
                                                             INNER JOIN
                                                                 EventAttendance EA ON U.Id = EA.UserId
                                                             INNER JOIN
                                                                 Posts P ON EA.PostId = P.Id
                                                             WHERE
                                                                 P.RecordDate BETWEEN @FromDate AND @ToDate
                                                         ),
                                                         InactiveCounts AS (
                                                             SELECT
                                                                 COUNT(*) AS InactiveCount
                                                             FROM
                                                                 Users U
                                                             WHERE
                                                                 NOT EXISTS (
                                                                     SELECT 1
                                                                     FROM EventAttendance EA
                                                                     INNER JOIN Posts P ON EA.PostId = P.Id
                                                                     WHERE U.Id = EA.UserId
                                                                       AND P.RecordDate BETWEEN @FromDate AND @ToDate
                                                                 )
                                                         )
                                                         SELECT
                                                             UC.TotalCount,
                                                             UC.NewCount,
                                                             AC.ActiveCount,
                                                             IC.InactiveCount
                                                         FROM
                                                             UserCounts UC
                                                         CROSS JOIN
                                                             ActiveCounts AC
                                                         CROSS JOIN
                                                             InactiveCounts IC;
                                                         """;

    public const string InsertUserChildQuery = @"INSERT INTO UserChildren (UserId, ChildName, Age)
                                                      VALUES(@UserId, @ChildName, @Age);";

    public const string UserChildrenByUserIdQuery = @"SELECT ChildName, Age FROM UserChildren WHERE UserId = @Id";
    public const string DeleteChildrenByIdQuery = "DELETE FROM UserChildren WHERE  UserId = @UserId";
}