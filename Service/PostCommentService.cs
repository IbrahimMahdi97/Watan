using Interfaces;
using Service.Interface;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class PostCommentService : IPostCommentService
{
    private readonly IRepositoryManager _repository;
    public PostCommentService(IRepositoryManager repository)
    {
        _repository = repository;
    }
    
    public async Task<int> Create(PostCommentForManiupulationDto postComment, int userId)
    {
        var result = await _repository.PostComment.Create(postComment, userId);
        return result;
    }

    public async Task<PostCommentDto> GetById(int commentId)
    {
        var comment = await _repository.PostComment.GetById(commentId);
        return comment;
    }

    public async Task Update(PostCommentForManiupulationDto postComment, int commentId)
    {
        await _repository.PostComment.Update(postComment, commentId);
    }

    public async Task<IEnumerable<PostCommentDto>> GetPostComments(int postId)
    {
        var comments = await _repository.PostComment.GetPostComments(postId);
        return comments;
    }

    public async Task Delete(int commentId)
    {
        await _repository.PostComment.Delete(commentId);
    }
}