using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/complaints")]
[ApiController]
public class ComplaintsController : ControllerBase
{
    private readonly IServiceManager _service;
    public ComplaintsController(IServiceManager service) => _service = service;
}