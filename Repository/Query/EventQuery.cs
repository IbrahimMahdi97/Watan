namespace Repository.Query;

public static class EventQuery
{
    public const string SelectEventsByParametersQuery =
        @"SELECT EventDetails.*, Posts.*, T.Description AS Town, P.Description AS Province, 
                                            (SELECT COUNT(EAT.UserId) FROM EventAttendance EAT WHERE EAT.PostId = Posts.Id) AS AttendanceCount, 
                                            (SELECT COUNT(POL.UserId) FROM PostLikes POL WHERE POL.PostId = Posts.Id) AS LikesCount, 
                                            (SELECT COUNT(POC.Id) FROM PostComments POC WHERE POC.PostId = Posts.Id) AS CommentsCount 
                                            FROM EventDetails 
                                            INNER JOIN Posts ON EventDetails.PostId = Posts.Id 
                                            INNER JOIN Provinces P on EventDetails.ProvinceId = P.Id 
                                            INNER JOIN Towns T on EventDetails.TownId = T.Id
                                            WHERE IIF(@ProvinceId = 0, 0, EventDetails.ProvinceId) = @ProvinceId AND
                                              IIF(@TownId = 0, 0, EventDetails.TownId) = @TownId AND
                                              IIF(@AddedByUserId = 0, 0, Posts.AddedByUserId) = @AddedByUserId AND
                                              (Posts.RecordDate BETWEEN @AddedFromDate AND @AddedToDate) AND
                                              (EventDetails.Date BETWEEN @FromEventDate AND @ToEventDate)
                                              AND Posts.IsDeleted = 0
                                              ORDER BY Posts.RecordDate DESC
                                              OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY";

    public const string SelectCountOfEventsByParametersQuery =
        @"SELECT COUNT(Posts.Id)
                    FROM EventDetails 
                    INNER JOIN Posts ON EventDetails.PostId = Posts.Id 
                    WHERE IIF(@ProvinceId = 0, 0, EventDetails.ProvinceId) = @ProvinceId AND
                                              IIF(@TownId = 0, 0, EventDetails.TownId) = @TownId AND
                                              IIF(@AddedByUserId = 0, 0, Posts.AddedByUserId) = @AddedByUserId AND
                                              (Posts.RecordDate BETWEEN @AddedFromDate AND @AddedToDate) AND
                                              (EventDetails.Date BETWEEN @FromEventDate AND @ToEventDate)
                                              AND Posts.IsDeleted = 0";

    public const string EventByIdQuery =
        @"SELECT EventDetails.*, Posts.*, T.Description AS Town, P.Description AS Province, 
                                            (SELECT COUNT(EAT.UserId) FROM EventAttendance EAT WHERE EAT.PostId = Posts.Id) AS AttendanceCount, 
                                            (SELECT COUNT(POL.UserId) FROM PostLikes POL WHERE POL.PostId = Posts.Id) AS LikesCount, 
                                            (SELECT COUNT(POC.Id) FROM PostComments POC WHERE POC.PostId = Posts.Id) AS CommentsCount 
                                            FROM EventDetails 
                                            INNER JOIN Posts ON EventDetails.PostId = Posts.Id 
                                            INNER JOIN Provinces P on EventDetails.ProvinceId = P.Id 
                                            INNER JOIN Towns T on EventDetails.TownId = T.Id WHERE PostId = @Id AND Posts.IsDeleted = 0";

    public const string InsertEvent =
        @"INSERT INTO EventDetails (PostId, Type, ProvinceId, TownId, Date, StartTime, EndTime, LocationUrl)
                                            VALUES (@PostId, @Type, @ProvinceId, @TownId, @Date, @StartTime, @EndTime, @LocationUrl)";

    public const string UpdateEventQuery = @"UPDATE EventDetails SET 
                                            Type=@Type, 
                                            ProvinceId=@ProvinceId, 
                                            TownId=@TownId, 
                                            StartTime=@StartTime, 
                                            EndTime=@EndTime, 
                                            LocationUrl=@LocationUrl
                                            WHERE PostId=@Id";

    public const string SelectAttendeesCountByProvinceIdAndTownIdQuery = """
                                                                         SELECT
                                                                             DATEADD(WEEK, DATEDIFF(WEEK, 0, P.RecordDate), 0) AS StartOfWeek,
                                                                             SUM(CASE WHEN U.provinceId = @ProvinceId AND U.townId = @TownId THEN 1 ELSE 0 END) AS CountOfInside,
                                                                             SUM(CASE WHEN U.provinceId != @ProvinceId OR U.townId != @TownId THEN 1 ELSE 0 END) AS CountOfOutside
                                                                         FROM
                                                                             EventAttendance A
                                                                         JOIN
                                                                             Users U ON A.UserId = U.Id
                                                                         JOIN
                                                                             Posts P ON A.PostId = P.Id
                                                                         WHERE
                                                                             (U.provinceId = @ProvinceId AND U.townId = @TownId)
                                                                             OR (U.provinceId != @ProvinceId OR U.townId != @TownId)
                                                                             AND P.IsDeleted = 0
                                                                         GROUP BY
                                                                             DATEADD(WEEK, DATEDIFF(WEEK, 0,P.RecordDate), 0)
                                                                         ORDER BY
                                                                             StartOfWeek;
                                                                         """;

    public const string SelectFromDateToDateQuery = """
                                                    SELECT P.Id AS PostId, P.Title, Pr.Description AS Province,
                                                    (SELECT COUNT(EAT.UserId) FROM EventAttendance EAT WHERE EAT.PostId = P.Id) AS AttendanceCount,
                                                        (SELECT COUNT(EAT.UserId) FROM EventAttendance EAT WHERE EAT.PostId = P.Id) * 1.0 / (SELECT COUNT(U.Id) FROM Users U WHERE U.ProvinceId = Ed.ProvinceId) AS AttendancePercentage
                                                        FROM Posts P
                                                        JOIN EventDetails ED ON P.Id = ED.PostId
                                                        JOIN Provinces Pr on ED.ProvinceId = Pr.Id
                                                        WHERE P.RecordDate BETWEEN @FromDate AND @ToDate
                                                        AND P.IsDeleted = 0
                                                    """;
}