using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace WatanPresentation.Controllers;

[Route("api/events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IServiceManager _service;
    public EventsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventWithPostDto>>> GetAllEvents()
    {
        var events = await _service.EventService.GetAllEvents();
        return Ok(events);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventWithPostDto>> GetEventById(int id)
    {
        var eventDetails = await _service.EventService.GetEventById(id);
        return Ok(eventDetails);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<ActionResult<EventDetails>> Create([FromForm] EventWithPostDto eventDto)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var eventDetails = await _service.EventService.Create(eventDto, userId);
        return Ok(eventDetails);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromForm] EventForManiupulationDto eventDto)
    {
        await _service.EventService.Update(id, eventDto);
        return NoContent();
    }
}