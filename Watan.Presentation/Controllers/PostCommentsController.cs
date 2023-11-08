using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/post_comments")]
[ApiController]
public class PostCommentsController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostCommentsController(IServiceManager service) => _service = service;
}