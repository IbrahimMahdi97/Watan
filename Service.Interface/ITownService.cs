using Shared.DataTransferObjects;

namespace Service.Interface;

public interface ITownService
{
    Task<IEnumerable<TownDto>> GetAll();
    Task<TownDto> GetById(int id);
    Task<TownDto> GetByName(string name);
    Task<IEnumerable<TownDto>> GetByProvince(int provinceId);
    Task<int> Create(TownForManipulationDto townDto);
    Task Update(int id, TownForManipulationDto townDto);
    Task Delete(int id);
}