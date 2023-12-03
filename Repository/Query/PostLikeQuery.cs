namespace Repository.Query;

public static class PostLikeQuery
{
    public const string InsertQuery = @"INSERT INTO PostLikes (PostId, UserId) 
                                        VALUES (@PostId, @UserId)";
    public const string DeleteQuery = @"DELETE FROM PostLikes WHERE PostId = @PostId AND UserId = @UserId";
    public const string CheckIfLikeExistQuery = @"SELECT PostId FROM PostLikes WHERE PostId = @PostId AND UserId = @UserId";
    public const string CountByPostId = @"SELECT COUNT(UserId) FROM PostLikes WHERE PostId = @PostId";
}