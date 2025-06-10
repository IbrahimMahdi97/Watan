using Entities.Exceptions;
using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class PostService : IPostService
{
    private readonly IRepositoryManager _repository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IConfiguration _configuration;

    public PostService(IRepositoryManager repository, IFileStorageService fileStorageService,
        IConfiguration configuration)
    {
        _repository = repository;
        _fileStorageService = fileStorageService;
        _configuration = configuration;
    }

    public async Task<IEnumerable<PostDto>> GetAllPosts(int userId)
    {
        var posts = (await _repository.Post.GetAllPosts(userId)).ToList();

        foreach (var post in posts)
        {
            var images = _fileStorageService.GetFilesUrlsFromServer(post.Id,
                _configuration["PostImagesSetStorageUrl"]!,
                _configuration["PostImagesGetStorageUrl"]!).ToList();

            post.ImageUrl = images.Any() ? images.First() : "";
        }

        return posts;
    }

    public async Task<PostDetailsDto> GetPostById(int id, int userId)
    {
        var post = await _repository.Post.GetPostById(id, userId);
        if (post is null) throw new PostNotFoundException(id);

        var images = _fileStorageService.GetFilesUrlsFromServer(post.Id,
            _configuration["PostImagesSetStorageUrl"]!,
            _configuration["PostImagesGetStorageUrl"]!).ToList();

        post.ImageUrl = images.Any() ? images.First() : "";
        var comments = await _repository.PostComment.GetPostComments(id, userId);
        var postComments = comments.ToList();
        foreach (var comment in postComments)
        {
            comment.Replies = await _repository.PostComment.GetCommentReplies(comment.Id, userId);
        }

        post.Comments = postComments;
        var likes = await _repository.PostLike.GetPostLikes(id);
        
        var likesArray = likes as LikeDto[] ?? likes.ToArray();
        foreach (var like in likesArray)
        {
            var userImages = _fileStorageService.GetFilesUrlsFromServer(like.UserId,
                _configuration["UserImagesSetStorageUrl"]!,
                _configuration["UserImagesGetStorageUrl"]!).ToList();

            like.UserImageUrl = userImages.Any() ? userImages.First() : "";
        }

        post.Likes = likesArray;

        return post;
    }

    public async Task<int> CreatePost(PostForManipulationDto postDto, int userId, string postType)
    {
        if (postDto.Title is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("Title", 50);

        var (result, connection, transaction) = await _repository.Post.CreatePost(postDto, userId, postType);

        //boolean flag can be added to move these lines into repo
        transaction.Commit();
        connection.Close();

        if (result > 0 && postDto.PostImage is not null)
            await _fileStorageService.CopyFileToServer(result,
                _configuration["PostImagesSetStorageUrl"]!, postDto.PostImage);

        return result;
    }

    public async Task UpdatePost(int id, PostForManipulationDto postDto)
    {
        await GetPostById(id, 0);
        await _repository.Post.UpdatePost(id, postDto);
        _fileStorageService.DeleteFilesFromServer(id, _configuration["PostImagesSetStorageUrl"]!);

        if (postDto.PostImage is null) return;

        await _fileStorageService.CopyFileToServer(id,
            _configuration["PostImagesSetStorageUrl"]!, postDto.PostImage);
    }

    public async Task DeletePost(int id)
    {
        await GetPostById(id, 0);
        await _repository.Post.DeletePost(id);
    }
}