using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IServiceManager _service;
    public EventsController(IServiceManager service) => _service = service;
}