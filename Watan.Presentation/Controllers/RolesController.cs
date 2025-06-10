using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;

namespace WatanPresentation.Controllers;

[Route("api/roles")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IServiceManager _service;
    public RolesController(IServiceManager service) => _service = service;
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserRoleDto>>> GetAll()
    {
        var roles = await _service.RoleService.GetAll();
        return Ok(roles);
    }
}