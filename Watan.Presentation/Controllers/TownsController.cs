using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/towns")]
[ApiController]
public class TownsController : ControllerBase
{
    private readonly IServiceManager _service;
    public TownsController(IServiceManager service) => _service = service;
}