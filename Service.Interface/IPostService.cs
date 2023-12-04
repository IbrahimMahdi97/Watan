using Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interface;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllPosts();
    Task<ActionResult<PostDetailsDto>> GetPostById(int id);
    Task<int> CreatePost(PostForManipulationDto postDto, int userId, string postType);
    Task UpdatePost(int id, PostForManipulationDto postDto);
    Task DeletePost(int id);
}