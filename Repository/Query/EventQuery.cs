namespace Repository.Query;

public static class EventQuery
{
    public const string AllEventsQuery = @"SELECT EventDetails.*, Posts.*
                                            FROM EventDetails 
                                            INNER JOIN Posts ON EventDetails.PostId = Posts.Id 
                                            WHERE Posts.IsDeleted = 0";
    
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