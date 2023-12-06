using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace WatanPresentation.Controllers;

[Route("api/towns")]
[ApiController]
public class TownsController : ControllerBase
{
    private readonly IServiceManager _service;
    public TownsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TownDto>>> GetByParameters([FromQuery] TownsParameters parameters)
    {
        var towns = await _service.TownService.GetByParameters(parameters);
        return Ok(towns);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TownDto>> GetById(int id)
    {
        var town = await _service.TownService.GetById(id);
        return Ok(town);
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<int>> Create(TownForManipulationDto townDto)
    {
        var id = await _service.TownService.Create(townDto);
        return Ok(id);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, TownForManipulationDto townDto)
    {
        await _service.TownService.Update(id, townDto);
        return NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.TownService.Delete(id);
        return NoContent();
    }
}