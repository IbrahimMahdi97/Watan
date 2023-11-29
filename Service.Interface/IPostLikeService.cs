namespace Service.Interface;

public interface IPostLikeService
{
    Task Create(int postId, int userId);
}