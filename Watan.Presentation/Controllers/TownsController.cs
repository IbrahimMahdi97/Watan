using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/towns")]
[ApiController]
public class TownsController : ControllerBase
{
    private readonly IServiceManager _service;
    public TownsController(IServiceManager service) => _service = service;
}