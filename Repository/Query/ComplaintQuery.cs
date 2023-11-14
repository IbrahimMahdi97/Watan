namespace Repository.Query;

public static class ComplaintQuery
{
    public const string AllComplaintsQuery = @"SELECT * FROM Complaint WHERE IsDeleted=0";
    public const string ComplaintByIdQuery = @"SELECT * FROM Complaint WHERE Id=@Id";

    public const string InsertComplaintQuery = @"INSERT INTO Complaint (UserId, TypeId, Details, RecordDate) 
                                                OUTPUT inserted.Id 
                                                VALUES (@UserId, @TypeId, @Details, @RecordDate)";

    public const string DeleteComplaintQuery = @"UPDATE Complaint SET IsDeleted=1 WHERE Id=@Id";
    public const string UpdateComplaintQuery = @"UPDATE Complaint SET Details=@Details, TypeId=@TypeId WHERE Id=@Id";
}