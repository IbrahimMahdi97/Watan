using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Watan.Presentation.Controllers;

[Route("api/event_details")]
[ApiController]
public class EventDetailsController : ControllerBase
{
    private readonly IServiceManager _service;
    public EventDetailsController(IServiceManager service) => _service = service;
}