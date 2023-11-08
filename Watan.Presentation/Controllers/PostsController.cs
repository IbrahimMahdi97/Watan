using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostsController(IServiceManager service) => _service = service;
}