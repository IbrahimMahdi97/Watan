using Interfaces;
using Service.Interface;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class ProvinceService : IProvinceService
{
    private readonly IRepositoryManager _repository;
    
    public ProvinceService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProvinceDto>> GetAll()
    {
        var provinces = await _repository.Province.GetAllProvinces();
        return provinces;
    }
    
    public async Task<ProvinceDto> GetById(int id)
    {
        var province = await _repository.Province.GetProvinceById(id);
        return province;
    }

    public async Task<ProvinceDto> GetByName(string name)
    {
        var province = await _repository.Province.GetProvinceByName(name);
        return province;
    }

    public async Task<int> Create(ProvinceForManipulationDto provinceDto)
    {
        var result = await _repository.Province.CreateProvince(provinceDto);
        return result;
    }

    public async Task Update(int id, ProvinceForManipulationDto province)
    {
        await _repository.Province.UpdateProvince(id, province);
    }

    public async Task Delete(int id)
    {
        await _repository.Province.DeleteProvince(id);
    }
}