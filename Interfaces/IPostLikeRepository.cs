using Shared.DataTransferObjects;

namespace Interfaces;

public interface IPostLikeRepository
{
    Task<bool> CheckIfExist(int postId, int userId);
    Task Create(int postId, int userId);
    Task Delete(int postId, int userId);

}