using Microsoft.AspNetCore.Authorization;
using Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Watan.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _service;
    public UsersController(IServiceManager service) => _service = service;
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserForCreationDto userForCreationDto)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var id = await _service.UserService.CreateUser(userForCreationDto, userId);
        return id > 0 ? Ok(new { Id = id }) : BadRequest();
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        var userDto = await _service.UserService.ValidateUser(user);
        return Ok(userDto);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        var userDto = await _service.UserService.GetById(id);
        return Ok(userDto);
    }
}