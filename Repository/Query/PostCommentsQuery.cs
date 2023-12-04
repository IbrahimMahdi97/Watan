namespace Repository.Query;

public static class PostCommentsQuery
{
    public const string InsertQuery = @"INSERT INTO PostComments 
                                        (PostId, UserId, Comment, ParentCommentId, RecordDate) 
                                        OUTPUT inserted.Id 
                                        VALUES (@PostId, @UserId, @Comment, @ParentCommentId, @RecordDate)";
    public const string GetAllQuery = @"SELECT * FROM PostComments WHERE IsDeleted=0";
    public const string GetByPostIdQuery = @"SELECT PC.PostId AS PostId, PC.Comment AS Comment, PC.Id AS Id, 
                                            PC.UserId AS UserId, PC.ParentCommentId AS ParentCommentId, 
                                            PC.RecordDate AS RecordDate, US.FullName AS FullName, 
                                            (SELECT COUNT(Id) FROM PostComments WHERE ParentCommentId=PC.Id) AS RepliesCount, 
                                            (SELECT COUNT(UserId) FROM CommentLikes WHERE CommentId=PC.Id) AS LikesCount 
                                            FROM PostComments PC INNER JOIN Users US ON (PC.UserId=US.Id) 
                                            WHERE PostId=@PostId AND PC.IsDeleted=0";

    public const string GetCommentRepliesQuery = @"SELECT PC.PostId AS PostId, PC.Comment AS Comment, PC.Id AS Id, 
                                            PC.UserId AS UserId, PC.ParentCommentId AS ParentCommentId, 
                                            PC.RecordDate AS RecordDate, US.FullName AS FullName, 
                                            (SELECT COUNT(Id) FROM PostComments WHERE ParentCommentId=PC.Id) AS RepliesCount, 
                                            (SELECT COUNT(UserId) FROM CommentLikes WHERE CommentId=PC.Id) AS LikesCount 
                                            FROM PostComments PC INNER JOIN Users US ON (PC.UserId=US.Id) 
                                            WHERE ParentCommentId=@CommentId AND PC.IsDeleted=0";
    public const string GetByIdQuery = @"SELECT * FROM PostComments WHERE Id=@Id";
    public const string UpdateQuery = @"Update PostComments SET Comment = @Comment WHERE Id = @Id";
    public const string DeleteQuery = @"Update PostComments SET IsDeleted=1 WHERE Id = @Id";
    public const string CountByPostId = @"SELECT COUNT(Id) FROM PostComments WHERE PostId=@PostId";
 /*   public const string AddCommentLikeQuery = @"INSERT INTO CommentLikes (CommentId, UserId) 
                                                VALUES (@CommentId, @UserId)";

    
    public const string DeleteCommentLikeQuery = @"DELETE FROM CommentLikes WHERE CommentId = @CommentId AND UserId = @UserId";
    public const string GetLikesCountQuery = @"SELECT COUNT(UserId) FROM CommentLikes WHERE CommentId = @CommentId";
    */
    
    public const string InsertOrDeleteQuery = @"
                                    IF EXISTS (SELECT CommentId FROM CommentLikes WHERE CommentId = @CommentId 
                                                    AND UserId = @UserId)
                                    DELETE FROM CommentLikes WHERE CommentId = @CommentId AND UserId = @UserId
                                    ELSE
                                    INSERT INTO CommentLikes (CommentId, UserId) 
                                                VALUES (@CommentId, @UserId)";


    public const string GetLikesDetailsQuery = @"SELECT CL.UserId AS UserId, US.FullName AS FullName 
                                                FROM CommentLikes CL INNER JOIN Users US ON (CL.UserId = US.Id) 
                                                WHERE CL.CommentId = @CommentId";
}