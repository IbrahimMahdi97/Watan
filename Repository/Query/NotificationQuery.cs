namespace Repository.Query;

public class NotificationQuery
{
    public const string InsertQuery = @"INSERT INTO Notifications (Title, Description, UserId, AddedByUserId)
              OUTPUT inserted.Id
              VALUES (@Title, @Description, @UserId, @AddedByUserId)";

    public const string SelectNewCountQuery = @"SELECT COUNT(Id)
                                                   FROM Notifications
                                                    WHERE UserId = @Id AND IsRead = 0 AND IsDeleted = 0";

    public const string SelectByIdQuery = @"SELECT N.Id, N.Title, N.Description, N.UserId, N.RecordDate, N.AddedByUserId, N.IsRead
                FROM Notifications N
                 WHERE Id = @id AND IsDeleted = 0";

    public const string UpdateIsReadQuery = @"UPDATE Notifications SET IsRead = 1 WHERE Id = @id";
    
    public const string SelectCountQuery = @"SELECT COUNT(Id)
                                                   FROM Notifications
                                                    WHERE IIF(@UserId = 0, 0, UserId) = @UserId OR UserId = 0;";
    
    public const string SelectByParametersQuery = @"SELECT * FROM Notifications
                                                    WHERE IIF(@UserId = 0, 0, UserId) = @UserId OR UserId = 0
                                                    ORDER BY RecordDate DESC
                                                    OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY;";
}