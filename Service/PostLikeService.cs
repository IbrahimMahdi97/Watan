using Interfaces;
using Service.Interface;

namespace Service;

internal sealed class PostLikeService : IPostLikeService
{
    private readonly IRepositoryManager _repository;
    public PostLikeService(IRepositoryManager repository)
    {
        _repository = repository;
    }
    
    public async Task AddLike(int postId, int userId)
    {
        await _repository.PostLike.AddLike(postId, userId);
        /*
        if (exist)
            await _repository.PostLike.Delete(postId, userId);
        await _repository.PostLike.Create(postId, userId);
    */
    }
}