using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/event_attendance")]
[ApiController]
public class EventAttendanceController : ControllerBase
{
    private readonly IServiceManager _service;
    public EventAttendanceController(IServiceManager service) => _service = service;

    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult> Create(int userId, int eventId)
    {
        await _service.EventAttendanceService.Create(userId, eventId);
        return NoContent();
    }
}