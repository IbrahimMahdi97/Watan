using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace WatanPresentation.Controllers;

[Route("api/regions")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IServiceManager _service;
    public RegionsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RegionDto>>> GetByParameters([FromQuery] RegionsParameters parameters)
    {
        var regions = await _service.RegionService.GetByParameters(parameters);
        return Ok(regions);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<RegionDto>> GetById(int id)
    {
        var region = await _service.RegionService.GetById(id);
        return Ok(region);
    }
    /*
    [Authorize]
    [HttpGet("name")]
    public async Task<ActionResult<RegionDto>> GetByName(string name)
    {
        var region = await _service.RegionService.GetByName(name);
        return Ok(region);
    }
    
    [Authorize]
    [HttpGet("town")]
    public async Task<ActionResult<IEnumerable<RegionDto>>> GetByTown(int town)
    {
        var regions = await _service.RegionService.GetByTownId(town);
        return Ok(regions);
    }*/
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<int>> Create(RegionForManipulationDto regionDto)
    {
        var id = await _service.RegionService.Create(regionDto);
        return Ok(id);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, RegionForManipulationDto regionDto)
    {
        await _service.RegionService.Update(id, regionDto);
        return NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.RegionService.Delete(id);
        return NoContent();
    }
}