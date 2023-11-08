using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/user_roles")]
[ApiController]
public class UserRolesController : ControllerBase
{
    private readonly IServiceManager _service;
    public UserRolesController(IServiceManager service) => _service = service;
}