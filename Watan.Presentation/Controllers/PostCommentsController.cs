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
    public async Task<ActionResult<int>> Create(PostCommentForManiupulationDto postComment)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var result = await _service.PostCommentService.Create(postComment, userId);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PostCommentDto>> GetById(int id)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var comment = await _service.PostCommentService.GetById(id, userId);
        return Ok(comment);
    }


    [Authorize]
    [HttpPut]
    public async Task<ActionResult> Update(PostCommentForManiupulationDto postComment, int commentId)
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
        var userId = User.RetrieveUserIdFromPrincipal();
        var comments = await _service.PostCommentService.GetPostComments(postId, userId);
        return Ok(comments);
    }
    
    [Authorize]
    [HttpPost("like")]
    public async Task<ActionResult> Create(int commentId)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        await _service.PostCommentService.Like(commentId, userId);
        return NoContent();
    }
    
    [Authorize]
    [HttpGet("comment-likes")]
    public async Task<ActionResult<IEnumerable<LikeDto>>> GetCommentsLikes(int commentId)
    {
        var comments = await _service.PostCommentService.GetLikes(commentId);
        return Ok(comments);
    }
}