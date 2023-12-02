namespace Repository.Query;

public static class PostCommentsQuery
{
    public const string InsertQuery = @"INSERT INTO PostComments 
                                        (PostId, UserId, Comment, ParentCommentId, RecordDate) 
                                        OUTPUT inserted.Id 
                                        VALUES (@PostId, @UserId, @Comment, @ParentCommentId, @RecordDate)";
    public const string GetAllQuery = @"SELECT * FROM PostComments WHERE IsDeleted=0";
    public const string GetByPostIdQuery = @"SELECT * FROM PostComments WHERE PostId=@PostId AND IsDeleted=0";
    public const string GetByIdQuery = @"SELECT * FROM PostComments WHERE Id=@Id";
    public const string UpdateQuery = @"Update PostComments SET Comment = @Comment WHERE Id = @Id";
    public const string DeleteQuery = @"Update PostComments SET IsDeleted=1 WHERE Id = @Id";
    public const string CountByPostId = @"SELECT COUNT(Id) FROM PostComments WHERE PostId=@PostId";
}