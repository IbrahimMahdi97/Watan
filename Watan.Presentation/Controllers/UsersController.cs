using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace WatanPresentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IServiceManager _service;
    public UsersController(IServiceManager service) => _service = service;
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromForm] UserForCreationDto userForCreationDto)
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

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDto>> GetUserDetails()
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var userDto = await _service.UserService.GetById(userId);
        return Ok(userDto);
    }
}