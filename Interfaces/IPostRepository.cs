using Entities.Models;
using Shared.DataTransferObjects;

namespace Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllPosts();
    Task<PostDto> GetPostById(int id);
    Task<int> CreatePost(PostForManipulationDto postDto, int userId);
    Task UpdatePost(int id, PostForManipulationDto postDto);
    Task DeletePost(int id);
}