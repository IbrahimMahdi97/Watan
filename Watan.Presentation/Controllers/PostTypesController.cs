using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/post_types")]
[ApiController]
public class PostTypesController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostTypesController(IServiceManager service) => _service = service;
}