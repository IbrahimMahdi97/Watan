using Entities.Exceptions;
using Interfaces;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class TownService : ITownService
{
    private readonly IRepositoryManager _repository;
    
    public TownService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TownDto>> GetByParameters(TownsParameters parameters)
    {
        if (parameters.ProvinceId > 0) await IsProvinceExist(parameters.ProvinceId);
        if (parameters.Id > 0) await GetById(parameters.Id);
        var towns = await _repository.Town.GetByParameters(parameters);
        return towns;
    }


    public async Task<TownDto> GetById(int id)
    {
        var town = await _repository.Town.GetById(id);
        if (town is null) throw new TownNotFoundException(id);
        return town;
    }

    public async Task<int> Create(TownForManipulationDto townDto)
    {
        await IsProvinceExist(townDto.ProvinceId);
        var result = await _repository.Town.Create(townDto);
        return result;
    }

    public async Task Update(int id, TownForManipulationDto townDto)
    {
        await IsProvinceExist(townDto.ProvinceId);
        await _repository.Town.Update(id, townDto);
    }

    public async Task Delete(int id)
    {
        var town = GetById(id);
        await _repository.Town.Delete(id);
    }

    private async Task IsProvinceExist(int provinceId)
    {
        var provinceService = new ProvinceService(_repository);
        var province = await provinceService.GetById(provinceId);
    }
}