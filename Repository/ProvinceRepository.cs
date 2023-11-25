using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

public class ProvinceRepository : IProvinceRepository
{
    private readonly DapperContext _context;

    public ProvinceRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ProvinceDto>> GetAllProvinces()
    {
        const string query = ProvinceQuery.AllProvincesQuery;
        using var connection = _context.CreateConnection();
        var provinces = await connection.QueryAsync<ProvinceDto>(query);
        return provinces.ToList();
    }
    
    public async Task<ProvinceDto> GetProvinceById(int id)
    {
        const string query = ProvinceQuery.ProvinceByIdQuery;
        using var connection = _context.CreateConnection();
        var province = await connection.QuerySingleOrDefaultAsync<ProvinceDto>(query, new { Id = id });
        return province;
    }
    
    public async Task<ProvinceDto> GetProvinceByName(string name)
    {
        const string query = ProvinceQuery.ProvinceByNameQuery;
        using var connection = _context.CreateConnection();
        var province = await connection.QuerySingleOrDefaultAsync<ProvinceDto>(query, new { Description = name });
        return province;
    }
    
    public async Task<int> CreateProvince(ProvinceForManipulationDto provinceDto)
    {
        const string query = ProvinceQuery.InsertProvinceQuery;
        var param = new DynamicParameters(provinceDto);
        
        using var connection = _context.CreateConnection();
        connection.Open();
        using var trans = connection.BeginTransaction();
        var id = await connection.QuerySingleAsync<int>(query, param, transaction: trans);
        if (id <= 0)
        {
            trans.Rollback();
            return 0;
        }
        trans.Commit();
        return id;
    }
    
    public async Task UpdateProvince(int id, ProvinceForManipulationDto provinceDto)
    {
        const string query = ProvinceQuery.UpdateProvinceQuery;
        var param = new DynamicParameters(provinceDto);
        param.Add("Id", id);
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, param);
    }
    
    public async Task DeleteProvince(int id)
    {
        const string query = ProvinceQuery.DeleteProvinceQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}