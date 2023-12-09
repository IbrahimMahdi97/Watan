using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

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
    public async Task<ActionResult<UserDetailsDto>> GetUserById(int id)
    {
        var userDto = await _service.UserService.GetById(id);
        return Ok(userDto);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("rating")]
    public async Task<ActionResult> UpdateUserRating(UserRatingForUpdateDto userRatingForUpdateDto)
    {
        await _service.UserService.UpdateRating(userRatingForUpdateDto);
        return NoContent();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserDetailsDto>> GetUserDetails()
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var userDto = await _service.UserService.GetById(userId);
        return Ok(userDto);
    }


    [HttpPost("refresh_token")]
    public async Task<ActionResult<TokenDto>> Refresh([FromBody] TokenDto tokenDto)
    {
        var token = await _service.UserService.RefreshToken(tokenDto);
        return Ok(token);
    }

    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDetailsDto>>> GetByParameters([FromQuery] UsersParameters parameters)
    {
        var users = await _service.UserService.GetByParameters(parameters);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(users.MetaData));
        return Ok(users);
    }
}