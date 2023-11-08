using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IServiceManager _service;
    public RolesController(IServiceManager service) => _service = service;
}