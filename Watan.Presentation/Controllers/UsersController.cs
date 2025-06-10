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

    [Authorize(Roles = "admin")]
    [HttpPut("{id:int}/children")]
    public async Task<IActionResult> AddUserChildren(IEnumerable<UserChildForManipulation> children, int id)
    {
        await _service.UserService.AddChildren(id, children);
        return NoContent();
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromForm] UserForCreationDto userForCreationDto)
    {
        await _service.UserService.Update(userForCreationDto, id);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Authenticate([FromBody] UserForAuthenticationDto user)
    {
        var userDto = await _service.UserService.ValidateUser(user);
        return Ok(userDto);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserForListingDto>>> GetUserByParameters([FromQuery] UsersParameters parameters)
    {
        var users = await _service.UserService.GetByParameters(parameters, false);
      //  Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(users.MetaData));
        return Ok(new {users, users.MetaData});
    }
    
    [Authorize]
    [HttpGet("deleted")]
    public async Task<ActionResult<IEnumerable<UserForListingDto>>> GetDeletedUserByParameters([FromQuery] UsersParameters parameters)
    {
        var users = await _service.UserService.GetByParameters(parameters, true);
      //  Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(users.MetaData));
        return Ok(new {users, users.MetaData});
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDetailsDto>> GetUserById(int id)
    {
        var userDto = await _service.UserService.GetById(id);
        return Ok(userDto);
    }
    
    [Authorize]
    [HttpGet("count/{provinceId:int}/{townId:int}")]
    public async Task<ActionResult<int>> GetUsersCountByProvinceIdAndTownId(int provinceId, int townId)
    {
        var count = await _service.UserService.GetCountByProvinceIdAndTownId(provinceId, townId);
        return Ok(count);
    }
    
    [Authorize]
    [HttpGet("count/from-{fromDate:datetime}-to-{toDate:datetime}")]
    public async Task<ActionResult<UsersCountDto>> GetUsersCountByDates(DateTime fromDate, DateTime toDate)
    {
        var count = await _service.UserService.GetCountFromDateToDate(fromDate, toDate);
        return Ok(count);
    }

    [Authorize(Roles = "admin")]
    [HttpPut("rating")]
    public async Task<ActionResult> UpdateUserRating(UserRatingForUpdateDto userRatingForUpdateDto)
    {
        await _service.UserService.UpdateRating(userRatingForUpdateDto);
        return NoContent();
    }
    
    [Authorize(Roles = "admin, manager")]
    [HttpPut("undelete/{id:int}")]
    public async Task<ActionResult> UndeleteUser(int id)
    {
        await _service.UserService.Undelete(id);
        return NoContent();
    }
    
    [Authorize(Roles = "admin, manager")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        await _service.UserService.Delete(id);
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
    
    [Authorize]
    [HttpGet("hierarchy")]
    public async Task<ActionResult<UserHierarchyDto>> GetHierarchy()
    {
        var hierarchy = await _service.UserService.GetHierarchy();
        return Ok(hierarchy);
    }


    [HttpPost("refresh_token")]
    public async Task<ActionResult<TokenDto>> Refresh([FromBody] TokenDto tokenDto)
    {
        var token = await _service.UserService.RefreshToken(tokenDto);
        return Ok(token);
    }
}