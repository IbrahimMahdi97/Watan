using Interfaces;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class RegionService : IRegionService
{
    private readonly IRepositoryManager _repository;
    
    public RegionService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RegionDto>> GetByParameters(RegionsParameters parameters)
    {
        var regions = await _repository.Region.GetByParameters(parameters);
        return regions;
    }
    
   public async Task<RegionDto> GetById(int id)
    {
        var region = await _repository.Region.GetById(id);
        return region;
    }

    public async Task<int> Create(RegionForManipulationDto regionDto)
    {
        var result = await _repository.Region.Create(regionDto);
        return result;
    }

    public async Task Update(int id, RegionForManipulationDto regionDto)
    {
        await _repository.Region.Update(id, regionDto);
    }

    public async Task Delete(int id)
    {
        await _repository.Region.Delete(id);
    }
}