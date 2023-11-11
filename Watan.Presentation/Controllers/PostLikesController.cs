using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/post_likes")]
[ApiController]
public class PostLikesController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostLikesController(IServiceManager service) => _service = service;
}