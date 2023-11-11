using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/event_details")]
[ApiController]
public class EventDetailsController : ControllerBase
{
    private readonly IServiceManager _service;
    public EventDetailsController(IServiceManager service) => _service = service;
}