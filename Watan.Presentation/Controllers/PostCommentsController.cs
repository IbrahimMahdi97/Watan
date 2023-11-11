using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/post_comments")]
[ApiController]
public class PostCommentsController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostCommentsController(IServiceManager service) => _service = service;
}