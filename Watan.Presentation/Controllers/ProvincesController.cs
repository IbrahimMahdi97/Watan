using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/provinces")]
[ApiController]
public class ProvincesController : ControllerBase
{
    private readonly IServiceManager _service;
    public ProvincesController(IServiceManager service) => _service = service;
}