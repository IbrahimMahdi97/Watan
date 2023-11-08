using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace Watan.Presentation.Controllers;

[Route("api/event_attendance")]
[ApiController]
public class EventAttendanceController : ControllerBase
{
    private readonly IServiceManager _service;
    public EventAttendanceController(IServiceManager service) => _service = service;
}