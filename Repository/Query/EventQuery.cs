namespace Repository.Query;

public static class EventQuery
{
    public const string AllEventsQuery = @"SELECT * FROM EventDetails;";
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