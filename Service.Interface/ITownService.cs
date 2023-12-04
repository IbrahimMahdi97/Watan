using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Interface;

public interface ITownService
{
    Task<IEnumerable<TownDto>> GetByParameters(TownsParameters parameters);
  
    Task<TownDto> GetById(int id);
     /* Task<TownDto> GetByName(string name);
    Task<IEnumerable<TownDto>> GetByProvince(int provinceId);
    */
    Task<int> Create(TownForManipulationDto townDto);
    Task Update(int id, TownForManipulationDto townDto);
    Task Delete(int id);
}