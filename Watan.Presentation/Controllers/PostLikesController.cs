using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.Helpers;

namespace WatanPresentation.Controllers;

[Route("api/post_likes")]
[ApiController]
public class PostLikesController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostLikesController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create([FromForm] int postId)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        await _service.PostLikeService.AddLike(postId, userId);
        return NoContent();
    }
}