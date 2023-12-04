using System.Collections;
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
        comment.Replies = await GetCommentRelies(comment.Id);
        return comment;
    }

    public async Task<IEnumerable<PostCommentDto>> GetCommentRelies(int commentId)
    {
        var comments = await _repository.PostComment.GetCommentReplies(commentId);
        return comments;
    }

    public async Task Update(PostCommentForManiupulationDto postComment, int commentId)
    {
        await _repository.PostComment.Update(postComment, commentId);
    }

    public async Task<IEnumerable<PostCommentDto>> GetPostComments(int postId)
    {
        var comments = await _repository.PostComment.GetPostComments(postId);
        var postComments = comments.ToList();
        foreach (var comment in postComments)
        {
            comment.Replies = await GetCommentRelies(comment.Id);
        }
        return postComments;
    }

    public async Task Delete(int commentId)
    {
        await _repository.PostComment.Delete(commentId);
    }

    public async Task Like(int commentId, int userId)
    {
        await _repository.PostComment.AddLike(commentId, userId);
        /*
        if (isLikeExist)
            await _repository.PostComment.RemoveLike(commentId, userId);
        await _repository.PostComment.AddLike(commentId, userId);
    */
    }

    public async Task<IEnumerable<LikeDto>> GetLikes(int commentId)
    {
        var likes = await _repository.PostComment.GetCommentLikes(commentId);
        return likes;
    }
}