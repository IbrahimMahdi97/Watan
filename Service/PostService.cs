using Interfaces;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<IEnumerable<PostDto>> GetAllPosts()
    {
        var posts = (await _repository.Post.GetAllPosts()).ToList();

        foreach (var post in posts)
        {
            var images = _fileStorageService.GetFilesUrlsFromServer(post.Id,
                _configuration["PostImagesSetStorageUrl"]!,
                _configuration["PostImagesGetStorageUrl"]!).ToList();

            post.ImageUrl = images.Any() ? images.First() : "";
        }

        return posts;
    }

    public async Task<ActionResult<PostDetailsDto>> GetPostById(int id)
    {
        var post = await _repository.Post.GetPostById(id);

        var images = _fileStorageService.GetFilesUrlsFromServer(post.Id,
            _configuration["PostImagesSetStorageUrl"]!,
            _configuration["PostImagesGetStorageUrl"]!).ToList();

        post.ImageUrl = images.Any() ? images.First() : "";
        post.Comments = await _repository.PostComment.GetPostComments(id);
        post.Likes = await _repository.PostLike.GetPostLikes(id);
        return post;
    }

    public async Task<int> CreatePost(PostForManipulationDto postDto, int userId, string postType)
    {
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
        await _repository.Post.UpdatePost(id, postDto);
        _fileStorageService.DeleteFilesFromServer(id, _configuration["PostImagesSetStorageUrl"]!);

        if (postDto.PostImage is null) return;

        await _fileStorageService.CopyFileToServer(id,
            _configuration["PostImagesSetStorageUrl"]!, postDto.PostImage);
    }

    public async Task DeletePost(int id)
    {
        await _repository.Post.DeletePost(id);
    }
}