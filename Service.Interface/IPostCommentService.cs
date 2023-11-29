using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IPostCommentService
{
    Task<int> Create(PostCommentForManiupulationDto postComment, int userId);
    Task<PostCommentDto> GetById(int commentId);
    Task Update(PostCommentForManiupulationDto postComment, int commentId);
    Task<IEnumerable<PostCommentDto>> GetPostComments(int postId);
    Task Delete(int commentId);
}