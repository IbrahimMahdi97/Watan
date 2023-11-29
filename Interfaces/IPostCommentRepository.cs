using Shared.DataTransferObjects;

namespace Interfaces;

public interface IPostCommentRepository
{
    Task<int> Create(PostCommentForManiupulationDto postComment, int userId);
    Task<PostCommentDto> GetById(int commentId);
    Task Update(PostCommentForManiupulationDto postComment, int commentId);
    Task<IEnumerable<PostCommentDto>> GetPostComments(int postId);
    Task Delete(int commentId);
}