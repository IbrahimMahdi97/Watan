using System.Collections;
using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IPostCommentService
{
    Task<int> Create(PostCommentForManiupulationDto postComment, int userId);
    Task<PostCommentDto> GetById(int commentId);
    Task Update(PostCommentForManiupulationDto postComment, int commentId);
    Task<IEnumerable<PostCommentDto>> GetPostComments(int postId);
    Task<IEnumerable<PostCommentDto>> GetCommentRelies(int commentId);
    Task Delete(int commentId);
    Task Like(int commentId, int userId);
    Task<IEnumerable<LikeDto>> GetLikes(int commentId);
}