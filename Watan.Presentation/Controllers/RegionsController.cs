using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/regions")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IServiceManager _service;
    public RegionsController(IServiceManager service) => _service = service;
}