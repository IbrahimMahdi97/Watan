using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/user_roles")]
[ApiController]
public class UserRolesController : ControllerBase
{
    private readonly IServiceManager _service;
    public UserRolesController(IServiceManager service) => _service = service;
}