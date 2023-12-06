using Shared.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Service.Interface;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllPosts(int userId);
    Task<PostDetailsDto> GetPostById(int id, int userId);
    Task<int> CreatePost(PostForManipulationDto postDto, int userId, string postType);
    Task UpdatePost(int id, PostForManipulationDto postDto);
    Task DeletePost(int id);
}