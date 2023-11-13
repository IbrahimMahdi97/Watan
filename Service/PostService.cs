using Entities.Models;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class PostService : IPostService
{
    private readonly IRepositoryManager _repository;
    private readonly IConfiguration _configuration;

    public PostService(IRepositoryManager repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    public async Task<IEnumerable<Post>> GetAllPosts()
    {
        var posts = await _repository.Post.GetAllPosts();
        return posts;
    }

    public async Task<ActionResult<PostDto>> GetPostById(int id)
    {
        var post = await _repository.Post.GetPostById(id);
        return post;
    }

    public async Task<int> CreatePost(PostForManipulationDto postDto, int userId)
    {
        var (result, connection, transaction) = await _repository.Post.CreatePost(postDto, userId);
        
        //boolean flag can be added to move these lines into repo
        transaction.Commit();
        connection.Close();
        
        return result;
    }

    public async Task UpdatePost(int id, PostForManipulationDto postDto)
    {
        await _repository.Post.UpdatePost(id, postDto);
    }

    public async Task DeletePost(int id)
    {
        await _repository.Post.DeletePost(id);
    }
}