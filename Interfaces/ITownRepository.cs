using Shared.DataTransferObjects;

namespace Interfaces;

public interface ITownRepository
{
    Task<IEnumerable<TownDto>> GetAll();
    Task<TownDto> GetById(int id);
    Task<TownDto> GetByName(string name);
    Task<IEnumerable<TownDto>> GetByProvinceId(int provinceId);
    Task<int> Create(TownForManipulationDto townDto);
    Task Update(int id, TownForManipulationDto townDto);
    Task Delete(int id);
}