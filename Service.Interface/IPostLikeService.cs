namespace Service.Interface;

public interface IPostLikeService
{
    Task AddLike(int postId, int userId);
}