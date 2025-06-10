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
        if (parameters.ProvinceId > 0) await ProvinceExist(parameters.ProvinceId);
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
        await ProvinceExist(townDto.ProvinceId);
        
        if (townDto.Description is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("Description", 50);

        var result = await _repository.Town.Create(townDto);
        return result;
    }

    public async Task Update(int id, TownForManipulationDto townDto)
    {
        await ProvinceExist(townDto.ProvinceId);
        await GetById(id);
        await _repository.Town.Update(id, townDto);
    }

    public async Task Delete(int id)
    {
        await GetById(id);
        await _repository.Town.Delete(id);
    }

    private async Task ProvinceExist(int provinceId)
    {
        var province = await _repository.Province.GetProvinceById(provinceId);
        if (province is null) throw new ProvinceNotFoundException(provinceId);
    }
}