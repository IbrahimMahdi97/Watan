using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace WatanPresentation.Controllers;

[Route("api/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        var posts = await _service.PostService.GetAllPosts();
        return Ok(posts);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostDto>> GetPostById(int id)
    {
        var post = await _service.PostService.GetPostById(id);
        return Ok(post);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<ActionResult<Post>> CreatePost(PostForManipulationDto postDto)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var plan = await _service.PostService.CreatePost(postDto, userId);
        return Ok(plan);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdatePost(int id, PostForManipulationDto postDto)
    {
        await _service.PostService.UpdatePost(id, postDto);
        return NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeletePost(int id)
    {
        await _service.PostService.DeletePost(id);
        return NoContent();
    }
}