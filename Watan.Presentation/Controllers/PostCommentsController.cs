using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace WatanPresentation.Controllers;

[Route("api/post_comments")]
[ApiController]
public class PostCommentsController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostCommentsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<int>> Create([FromForm] PostCommentForManiupulationDto postComment)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var result = await _service.PostCommentService.Create(postComment, userId);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostCommentDto>> GetById(int commentId)
    {
        var comment = await _service.PostCommentService.GetById(commentId);
        return Ok(comment);
    }


    [Authorize]
    [HttpPut]
    public async Task<ActionResult> Update([FromForm] PostCommentForManiupulationDto postComment, int commentId)
    {
        await _service.PostCommentService.Update(postComment, commentId);
        return NoContent();
    }

    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> Delete(int commentId)
    {
        await _service.PostCommentService.Delete(commentId);
        return NoContent();
    }

    [Authorize]
    [HttpGet("post-comments")]
    public async Task<ActionResult<IEnumerable<PostCommentDto>>> GetPostComments(int postId)
    {
        var comments = await _service.PostCommentService.GetPostComments(postId);
        return Ok(comments);
    }
    
    [Authorize]
    [HttpPost("like")]
    public async Task<ActionResult> Create([FromForm] int commentId)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        await _service.PostCommentService.Like(commentId, userId);
        return NoContent();
    }
}