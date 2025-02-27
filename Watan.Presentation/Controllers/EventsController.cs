using System.Text.Json;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

namespace WatanPresentation.Controllers;

[Route("api/events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IServiceManager _service;
    public EventsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventWithPostDto>>> GetAllEvents([FromQuery] EventsParameters eventsParameters)
    {
        var events = await _service.EventService.GetAllEvents(eventsParameters);
     // Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(events.MetaData));
        return Ok(new {events, events.MetaData});
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventWithPostDto>> GetEventById(int id)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var eventDetails = await _service.EventService.GetEventById(id, userId);
        return Ok(eventDetails);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost("create")]
    public async Task<ActionResult<EventDetails>> Create([FromForm] EventWithPostForCreationDto eventDto)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var eventDetails = await _service.EventService.Create(eventDto, userId);
        return Ok(eventDetails);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromForm] EventWithPostForCreationDto eventDto)
    {
        await _service.EventService.Update(id, eventDto);
        return NoContent();
    }
    
    [Authorize]
    [HttpGet("count-of-attendees/{provinceId:int}/{townId:int}")]
    public async Task<ActionResult<IEnumerable<AttendeesCountDto>>> GetAttendeesCountByProvinceIdAndTownId(int provinceId, int townId)
    {
        var counts = await _service.EventService.GetAttendeesCountByProvinceIdAndTownId(provinceId, townId);
        return Ok(counts);
    }
    
    [Authorize]
    [HttpGet("statistics/from-{fromDate:datetime}-to-{toDate:datetime}")]
    public async Task<ActionResult<IEnumerable<EventDto>>> GetEventsFromDateToDate(DateTime fromDate, DateTime toDate)
    {
        var events = await _service.EventService.GetFromDateToDate(fromDate, toDate);
        return Ok(events);
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.PostService.DeletePost(id);
        return NoContent();
    }
}