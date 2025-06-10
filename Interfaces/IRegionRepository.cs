using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Interfaces;

public interface IRegionRepository
{
    Task<IEnumerable<RegionDto>> GetByParameters(RegionsParameters parameters);
    
    Task<RegionDto> GetById(int id);
  /*  Task<RegionDto> GetByName(string name);
   
    Task<IEnumerable<RegionDto>> GetByTownId(int townId); */
    Task<int> Create(RegionForManipulationDto regionDto);
    Task Update(int id, RegionForManipulationDto regionDto);
    Task Delete(int id);
}