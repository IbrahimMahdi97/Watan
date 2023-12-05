using Entities.Exceptions;
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
        if (parameters.TownId > 0) await IsTownExist(parameters.TownId);
        if (parameters.Id > 0) await GetById(parameters.Id);
        var regions = await _repository.Region.GetByParameters(parameters);
        return regions;
    }
    
   public async Task<RegionDto> GetById(int id)
    {
        var region = await _repository.Region.GetById(id);
        if (region is null) throw new RegionNotFoundException(id);
        return region;
    }

    public async Task<int> Create(RegionForManipulationDto regionDto)
    {
        await IsTownExist(regionDto.TownId);
        var result = await _repository.Region.Create(regionDto);
        return result;
    }

    public async Task Update(int id, RegionForManipulationDto regionDto)
    {
        await IsTownExist(regionDto.TownId);
        await _repository.Region.Update(id, regionDto);
    }

    public async Task Delete(int id)
    {
        var region = GetById(id); 
        await _repository.Region.Delete(id);
    }

    private async Task IsTownExist(int townId)
    {
        var townService = new TownService(_repository);
        var town = await townService.GetById(townId);
    }
}