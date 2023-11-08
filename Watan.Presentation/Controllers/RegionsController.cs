using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/regions")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IServiceManager _service;
    public RegionsController(IServiceManager service) => _service = service;
}