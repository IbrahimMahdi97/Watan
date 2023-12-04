using Shared.DataTransferObjects;

namespace Interfaces;

public interface IPostLikeRepository
{
    Task AddLike(int postId, int userId);
    /*
    Task Create(int postId, int userId);
    Task Delete(int postId, int userId);
    */
    Task<IEnumerable<LikeDto>> GetPostLikes(int postId);
}