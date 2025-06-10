using Entities.Exceptions;
using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class PostCommentService : IPostCommentService
{
    private readonly IRepositoryManager _repository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IConfiguration _configuration;

    public PostCommentService(IRepositoryManager repository, IFileStorageService fileStorageService,
        IConfiguration configuration)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
        _configuration = configuration;
    }

    public async Task<int> Create(PostCommentForManiupulationDto postComment, int userId)
    {
        var post = await _repository.Post.GetPostById(postComment.PostId, userId);
        if (post is null) throw new PostNotFoundException(postComment.PostId);
        var result = await _repository.PostComment.Create(postComment, userId);
        return result;
    }

    public async Task<PostCommentDto> GetById(int commentId, int userId)
    {
        var comment = await _repository.PostComment.GetById(commentId);
        if (comment is null) throw new CommentNotFoundException(commentId);
        
        var replays = await _repository.PostComment.GetCommentReplies(comment.Id, userId);
        var commentReplays = replays as PostCommentDto[] ?? replays.ToArray();
        foreach (var replay in commentReplays)
        {
            var replayImages = _fileStorageService.GetFilesUrlsFromServer(comment.UserId,
                _configuration["UserImagesSetStorageUrl"]!,
                _configuration["UserImagesGetStorageUrl"]!).ToList();

            replay.UserImageUrl = replayImages.Any() ? replayImages.First() : "";
        }

        comment.Replies = commentReplays;
        
        var images = _fileStorageService.GetFilesUrlsFromServer(comment.UserId,
            _configuration["UserImagesSetStorageUrl"]!,
            _configuration["UserImagesGetStorageUrl"]!).ToList();

        comment.UserImageUrl = images.Any() ? images.First() : "";

        return comment;
    }
    
    public async Task Update(PostCommentForManiupulationDto postComment, int commentId)
    {
        await GetById(commentId, 0);
        await _repository.PostComment.Update(postComment, commentId);
    }

    public async Task<IEnumerable<PostCommentDto>> GetPostComments(int postId, int userId)
    {
        var post = await _repository.Post.GetPostById(postId, userId);
        if (post is null) throw new PostNotFoundException(postId);
        var comments = await _repository.PostComment.GetPostComments(postId, userId);
        var postComments = comments.ToList();
        foreach (var comment in postComments)
        {
            var replays = await _repository.PostComment.GetCommentReplies(comment.Id, userId);
            var commentReplays = replays as PostCommentDto[] ?? replays.ToArray();
            foreach (var replay in commentReplays)
            {
                var replayImages = _fileStorageService.GetFilesUrlsFromServer(comment.UserId,
                    _configuration["UserImagesSetStorageUrl"]!,
                    _configuration["UserImagesGetStorageUrl"]!).ToList();

                replay.UserImageUrl = replayImages.Any() ? replayImages.First() : "";
            }
            
            comment.Replies = commentReplays;
            
            var images = _fileStorageService.GetFilesUrlsFromServer(comment.UserId,
                _configuration["UserImagesSetStorageUrl"]!,
                _configuration["UserImagesGetStorageUrl"]!).ToList();

            comment.UserImageUrl = images.Any() ? images.First() : "";
        }

        return postComments;
    }

    public async Task Delete(int commentId)
    {
        var comment = await GetById(commentId, 0);
        await _repository.PostComment.Delete(commentId);
    }

    public async Task Like(int commentId, int userId)
    {
        var comment = await GetById(commentId, 0);
        await _repository.PostComment.AddLike(commentId, userId);
    }

    public async Task<IEnumerable<LikeDto>> GetLikes(int commentId)
    {
        var comment = await GetById(commentId, 0);
        var likes = await _repository.PostComment.GetCommentLikes(commentId);
        var likesArray = likes as LikeDto[] ?? likes.ToArray();
        foreach (var like in likesArray)
        {
            var images = _fileStorageService.GetFilesUrlsFromServer(like.UserId,
                _configuration["UserImagesSetStorageUrl"]!,
                _configuration["UserImagesGetStorageUrl"]!).ToList();

            like.UserImageUrl = images.Any() ? images.First() : "";
        }

        return likesArray;
    }
}