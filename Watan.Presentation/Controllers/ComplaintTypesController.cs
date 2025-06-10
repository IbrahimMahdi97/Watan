using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/complaint_types")]
[ApiController]
public class ComplaintTypesController : ControllerBase
{
    private readonly IServiceManager _service;
    public ComplaintTypesController(IServiceManager service) => _service = service;
}