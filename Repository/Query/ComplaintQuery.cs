namespace Repository.Query;

public static class ComplaintQuery
{
    public const string AllComplaintsQuery = @"SELECT * FROM Complaints WHERE Details LIKE '%' + @Search + '%' AND IsDeleted=0";
    public const string ComplaintsByParametersQuery = @"SELECT Co.Details AS Details, Co.RecordDate AS RecordDate, 
                                                        Co.Id AS Id, Co.UserId AS UserId, Co.TypeId AS TypeId, 
                                                        Type.Description AS ComplaintType
                                                        FROM Complaints Co 
                                                        INNER JOIN ComplaintTypes Type ON  Co.TypeId = Type.Id 
                                                        WHERE IIF(@TypeId = 0, 0, Co.TypeId) = @TypeId AND 
                                                              IIF(@UserId = 0, 0, Co.UserId) = @UserId AND 
                                                        (@Details IS NULL OR Co.Details LIKE '%' + @Details + '%') AND 
                                                        Co.IsDeleted = 0 
                                                        ORDER BY Co.RecordDate DESC
                                                        OFFSET @Skip ROWS FETCH NEXT @PageSize ROWS ONLY";
    public const string ComplaintsCountByParametersQuery = @"SELECT COUNT(Id)
                                                                FROM Complaints  
                                                                WHERE IIF(@TypeId = 0, 0, TypeId) = @TypeId AND 
                                                                      IIF(@UserId = 0, 0, UserId) = @UserId AND 
                                                                (@Details IS NULL OR Details LIKE '%' + @Details + '%')  AND 
                                                                IsDeleted = 0";
    public const string ComplaintByIdQuery = @"SELECT * FROM Complaints WHERE Id=@Id";

    public const string InsertComplaintQuery = @"INSERT INTO Complaints (UserId, TypeId, Details, RecordDate) 
                                                OUTPUT inserted.Id 
                                                VALUES (@UserId, @TypeId, @Details, @RecordDate)";

    public const string DeleteComplaintQuery = @"UPDATE Complaints SET IsDeleted=1 WHERE Id=@Id";
    public const string UpdateComplaintQuery = @"UPDATE Complaints SET Details=@Details, TypeId=@TypeId, Status=@Status WHERE Id=@Id";
    public const string GetComplaintsByUserId = @"SELECT * FROM Complaints WHERE UserId=@UserId AND IsDeleted=0";
}