using System.Data;
using Shared.DataTransferObjects;

namespace Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<PostDto>> GetAllPosts(int userId);
    Task<PostDetailsDto> GetPostById(int id);
    Task<(int, IDbConnection, IDbTransaction)> CreatePost(PostForManipulationDto postDto, int userId, string postType);
    Task UpdatePost(int id, PostForManipulationDto postDto);
    Task DeletePost(int id);
}