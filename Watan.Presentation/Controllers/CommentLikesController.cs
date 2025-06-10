using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/comment_likes")]
[ApiController]
public class CommentLikesController : ControllerBase
{
    private readonly IServiceManager _service;
    public CommentLikesController(IServiceManager service) => _service = service;
}