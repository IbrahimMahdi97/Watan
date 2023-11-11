using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostsController(IServiceManager service) => _service = service;
}