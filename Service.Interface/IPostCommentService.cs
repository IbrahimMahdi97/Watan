using System.Collections;
using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IPostCommentService
{
    Task<int> Create(PostCommentForManiupulationDto postComment, int userId);
    Task<PostCommentDto> GetById(int commentId, int userId);
    Task Update(PostCommentForManiupulationDto postComment, int commentId);
    Task<IEnumerable<PostCommentDto>> GetPostComments(int postId, int userId);
    Task Delete(int commentId);
    Task Like(int commentId, int userId);
    Task<IEnumerable<LikeDto>> GetLikes(int commentId);
}