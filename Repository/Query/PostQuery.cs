namespace Repository.Query;

public static class PostQuery
{
    public const string AllPostsQuery = @"SELECT Posts.*, 
                                (SELECT COUNT(POL.UserId) FROM PostLikes POL WHERE POL.PostId = Posts.Id) AS LikesCount, 
                                (SELECT COUNT(POC.Id) FROM PostComments POC WHERE POC.PostId = Posts.Id) AS CommentsCount,
                                 IIF((SELECT 1 FROM PostLikes WHERE PostId = Posts.Id AND UserId = @UserId) = 1, 1, 0) AS IsLikedByLoggedInUser 
                                            FROM Posts 
                                            WHERE Posts.IsDeleted=0 AND TypeId = @TypeId";
    
    public const string PostById = @"SELECT *, 
                                (SELECT COUNT(POL.UserId) FROM PostLikes POL WHERE POL.PostId = Posts.Id) AS LikesCount, 
                                (SELECT COUNT(POC.Id) FROM PostComments POC WHERE POC.PostId = Posts.Id) AS CommentsCount,
                                   IIF((SELECT 1 FROM PostLikes WHERE PostId = Posts.Id AND UserId = @UserId) = 1, 1, 0) AS IsLikedByLoggedInUser   
                                FROM Posts WHERE Id = @id";
    
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