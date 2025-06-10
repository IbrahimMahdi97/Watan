using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Interfaces;

public interface ITownRepository
{
    Task<IEnumerable<TownDto>> GetByParameters(TownsParameters parameters);
 
    Task<TownDto> GetById(int id);
     /*  Task<TownDto> GetByName(string name);
    Task<IEnumerable<TownDto>> GetByProvinceId(int provinceId);
    */
    Task<int> Create(TownForManipulationDto townDto);
    Task Update(int id, TownForManipulationDto townDto);
    Task Delete(int id);
}