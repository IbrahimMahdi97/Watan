using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/provinces")]
[ApiController]
public class ProvincesController : ControllerBase
{
    private readonly IServiceManager _service;
    public ProvincesController(IServiceManager service) => _service = service;
}