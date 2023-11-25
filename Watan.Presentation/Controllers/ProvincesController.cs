using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;

namespace WatanPresentation.Controllers;

[Route("api/provinces")]
[ApiController]
public class ProvincesController : ControllerBase
{
    private readonly IServiceManager _service;
    public ProvincesController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProvinceDto>>> GetAll()
    {
        var provinces = await _service.ProvinceService.GetAll();
        return Ok(provinces);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProvinceDto>> GetById(int id)
    {
        var province = await _service.ProvinceService.GetById(id);
        return Ok(province);
    }
    
    [Authorize]
    [HttpGet("{name}")]
    public async Task<ActionResult<ProvinceDto>> GetByName(string name)
    {
        var province = await _service.ProvinceService.GetByName(name);
        return Ok(province);
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<ActionResult<int>> Create([FromForm] ProvinceForManipulationDto provinceDto)
    {
        var id = await _service.ProvinceService.Create(provinceDto);
        return Ok(id);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, ProvinceForManipulationDto provinceDto)
    {
        await _service.ProvinceService.Update(id, provinceDto);
        return NoContent();
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.ProvinceService.Delete(id);
        return NoContent();
    }
}