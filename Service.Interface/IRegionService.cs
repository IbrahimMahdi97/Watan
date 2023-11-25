using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IRegionService
{
    Task<IEnumerable<RegionDto>> GetAll();
    Task<RegionDto> GetById(int id);
    Task<RegionDto> GetByName(string name);
    Task<IEnumerable<RegionDto>> GetByTownId(int townId);
    Task<int> Create(RegionForManipulationDto regionDto);
    Task Update(int id, RegionForManipulationDto regionDto);
    Task Delete(int id);
}