using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

namespace WatanPresentation.Controllers;

[Route("api/complaints")]
[ApiController]
public class ComplaintsController : ControllerBase
{
    private readonly IServiceManager _service;
    public ComplaintsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComplaintDto>>> GetAll([FromQuery] ComplaintsParameters parameters)
    {
        var complaints = await _service.ComplaintService.GetAllComplaints(parameters);
        return Ok(complaints);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ComplaintDto>> GetById(int id)
    {
        var complaint = await _service.ComplaintService.GetComplaintById(id);
        return Ok(complaint);
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<int>> Create([FromForm] ComplaintForManipulationDto complaintDto)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var id = await _service.ComplaintService.CreateComplaint(complaintDto, userId);
        return Ok(id);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, ComplaintForUpdateDto complaintDto)
    {
        await _service.ComplaintService.UpdateComplaint(id, complaintDto);
        return NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.ComplaintService.DeleteComplaint(id);
        return NoContent();
    }

    [Authorize]
    [HttpGet("my_complaints")]
    public async Task<ActionResult<MyComplaintsDto>> GetUserComplaints([FromQuery] ComplaintsParameters parameters)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var complaints = await _service.ComplaintService.GetUserComplaints(userId, parameters);
        return Ok(complaints);
    }
}