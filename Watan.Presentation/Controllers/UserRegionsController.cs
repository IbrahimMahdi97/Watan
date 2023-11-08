using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/user_regions")]
[ApiController]
public class UserRegionsController : ControllerBase
{
    private readonly IServiceManager _service;
    public UserRegionsController(IServiceManager service) => _service = service;
}