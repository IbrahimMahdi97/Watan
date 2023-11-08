using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _service;
    public UsersController(IServiceManager service) => _service = service;
    
}