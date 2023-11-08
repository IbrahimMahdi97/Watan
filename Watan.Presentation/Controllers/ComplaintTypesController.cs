using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/complaint_types")]
[ApiController]
public class ComplaintTypesController : ControllerBase
{
    private readonly IServiceManager _service;
    public ComplaintTypesController(IServiceManager service) => _service = service;
}