using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/post_likes")]
[ApiController]
public class PostLikesController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostLikesController(IServiceManager service) => _service = service;
}