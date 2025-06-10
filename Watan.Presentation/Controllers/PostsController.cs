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
    public async Task<ActionResult<IEnumerable<PostDto>>> GetAllPosts()
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var posts = await _service.PostService.GetAllPosts(userId);
        return Ok(posts);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostDetailsDto>> GetPostById(int id)
    {        
        var userId = User.RetrieveUserIdFromPrincipal();
        var post = await _service.PostService.GetPostById(id, userId);
        return Ok(post);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<ActionResult<Post>> CreatePost([FromForm] PostForManipulationDto postDto)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var plan = await _service.PostService.CreatePost(postDto, userId, "NWS");
        return Ok(plan);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdatePost(int id, [FromForm] PostForManipulationDto postDto)
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