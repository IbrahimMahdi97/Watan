namespace Repository.Query;

public static class ComplaintQuery
{
    public const string AllComplaintsQuery = @"SELECT * FROM Complaints WHERE IsDeleted=0";
    public const string ComplaintByIdQuery = @"SELECT * FROM Complaints WHERE Id=@Id";

    public const string InsertComplaintQuery = @"INSERT INTO Complaints (UserId, TypeId, Details, RecordDate) 
                                                OUTPUT inserted.Id 
                                                VALUES (@UserId, @TypeId, @Details, @RecordDate)";

    public const string DeleteComplaintQuery = @"UPDATE Complaints SET IsDeleted=1 WHERE Id=@Id";
    public const string UpdateComplaintQuery = @"UPDATE Complaints SET Details=@Details, TypeId=@TypeId WHERE Id=@Id";
    public const string GetComplaintsByUserId = @"SELECT * FROM Complaints WHERE UserId=@UserId AND IsDeleted=0";
}