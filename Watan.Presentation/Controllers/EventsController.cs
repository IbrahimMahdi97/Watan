using Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Watan.Presentation.Controllers;

[Route("api/events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IServiceManager _service;
    public EventsController(IServiceManager service) => _service = service;
}