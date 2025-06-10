using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Interface;

public interface IProvinceService
{
    Task<IEnumerable<ProvinceDto>> GetByParameters(ProvincesParameters parameters);
   Task<ProvinceDto> GetById(int id);
  //  Task<ProvinceDto> GetByName(string name);
    Task<int> Create(ProvinceForManipulationDto provinceDto);
    Task Update(int id, ProvinceForManipulationDto province);
    Task Delete(int id);
}