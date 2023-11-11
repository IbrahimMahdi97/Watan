using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IServiceManager _service;
    public RolesController(IServiceManager service) => _service = service;
}