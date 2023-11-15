using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace WatanPresentation.Controllers;

[Route("api/complaints")]
[ApiController]
public class ComplaintsController : ControllerBase
{
    private readonly IServiceManager _service;
    public ComplaintsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComplaintDto>>> GetAll()
    {
        var complaints = await _service.ComplaintService.GetAllComplaints();
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
    public async Task<ActionResult<int>> Create(ComplaintForManipulationDto complaintDto)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var id = await _service.ComplaintService.CreateComplaint(complaintDto, userId);
        return Ok(id);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, ComplaintForManipulationDto complaintDto)
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
}