using Interfaces;
using Service.Interface;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class TownService : ITownService
{
    private readonly IRepositoryManager _repository;
    
    public TownService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TownDto>> GetAll()
    {
        var towns = await _repository.Town.GetAll();
        return towns;
    }
    
    public async Task<TownDto> GetById(int id)
    {
        var town = await _repository.Town.GetById(id);
        return town;
    }

    public async Task<TownDto> GetByName(string name)
    {
        var town = await _repository.Town.GetByName(name);
        return town;
    }

    public async Task<IEnumerable<TownDto>> GetByProvince(int provinceId)
    {
        var towns = await _repository.Town.GetByProvinceId(provinceId);
        return towns;
    }

    public async Task<int> Create(TownForManipulationDto townDto)
    {
        var result = await _repository.Town.Create(townDto);
        return result;
    }

    public async Task Update(int id, TownForManipulationDto townDto)
    {
        await _repository.Town.Update(id, townDto);
    }

    public async Task Delete(int id)
    {
        await _repository.Town.Delete(id);
    }
}