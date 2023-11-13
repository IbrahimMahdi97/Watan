using Shared.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interface;

public interface IPostService
{
    Task<IEnumerable<Post>> GetAllPosts();
    Task<ActionResult<PostDto>> GetPostById(int id);
    Task<int> CreatePost(PostForManipulationDto postDto, int userId);
    Task UpdatePost(int id, PostForManipulationDto postDto);
    Task DeletePost(int id);
}