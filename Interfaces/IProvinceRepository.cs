using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Interfaces;

public interface IProvinceRepository
{
    Task<int> CreateProvince(ProvinceForManipulationDto provinceDto);
    Task<IEnumerable<ProvinceDto>> GetByParameters(ProvincesParameters parameters);
 
    Task<ProvinceDto> GetProvinceById(int id);
     /*  Task<ProvinceDto> GetProvinceByName(string name);
    */
    Task UpdateProvince(int id, ProvinceForManipulationDto provinceDto);
    Task DeleteProvince(int id);
}