using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/complaints")]
[ApiController]
public class ComplaintsController : ControllerBase
{
    private readonly IServiceManager _service;
    public ComplaintsController(IServiceManager service) => _service = service;
}