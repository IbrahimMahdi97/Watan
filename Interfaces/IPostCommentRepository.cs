using Shared.DataTransferObjects;

namespace Interfaces;

public interface IPostCommentRepository
{
    Task<int> Create(PostCommentForManiupulationDto postComment, int userId);
    Task<PostCommentDto> GetById(int commentId);
    Task Update(PostCommentForManiupulationDto postComment, int commentId);
    Task<IEnumerable<PostCommentDto>> GetPostComments(int postId, int userId);
    Task Delete(int commentId);
    /*
    Task AddLike(int commentId, int userId);
    Task RemoveLike(int commentId, int userId);
    */
    Task AddLike(int commentId, int userId);
    Task<IEnumerable<LikeDto>> GetCommentLikes(int commentId);
    Task<IEnumerable<PostCommentDto>> GetCommentReplies(int commentId, int userId);
}