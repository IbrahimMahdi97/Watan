namespace Repository.Query;

public static class PostQuery
{
    public const string AllPostsQuery = @"SELECT * FROM Posts WHERE IsDeleted=0 AND TypeId = @TypeId";
    
    public const string PostById = @"SELECT * FROM Posts WHERE Id = @id";
    
    public const string InsertPostQuery = @"INSERT INTO Posts (Title, Description, TypeId, AddedByUserId, RecordDate) 
                                            OUTPUT inserted.Id 
                                            VALUES (@Title, @Description, @TypeId, @AddedByUserId, @CreateDate);";
    
    public const string UpdatePostQuery = @"UPDATE Posts SET 
                                            Title = @Title,
                                            Description = @Description 
                                            WHERE Id = @Id;";

    public const string DeletePostQuery = @"Update Posts Set IsDeleted=1";

    public const string PostTypeIdQuery = @"SELECT Id FROM PostTypes WHERE Prefix=@Prefix";
}