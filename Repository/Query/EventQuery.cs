namespace Repository.Query;

public static class EventQuery
{
    public const string SelectEventsByParametersQuery = @"SELECT EventDetails.*, Posts.*
                                            FROM EventDetails 
                                            INNER JOIN Posts ON EventDetails.PostId = Posts.Id 
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
    
    public const string EventByIdQuery = @"SELECT * FROM EventDetails WHERE PostId = @Id";

    public const string InsertEvent = @"INSERT INTO EventDetails (PostId, Type, ProvinceId, TownId, Date, StartTime, EndTime, LocationUrl)
                                            VALUES (@PostId, @Type, @ProvinceId, @TownId, @Date, @StartTime, @EndTime, @LocationUrl)";

    public const string UpdateEventQuery = @"UPDATE EventDetails SET 
                                            Type=@Type, 
                                            ProvinceId=@ProvinceId, 
                                            TownId=@TownId, 
                                            StartTime=@StartTime, 
                                            EndTime=@EndTime, 
                                            LocationUrl=@LocationUrl
                                            WHERE PostId=@Id";
}