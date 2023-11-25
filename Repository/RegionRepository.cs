using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

public class RegionRepository : IRegionRepository
{
    private readonly DapperContext _context;

    public RegionRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<RegionDto>> GetAll()
    {
        const string query = RegionQuery.AllRegionsQuery;
        using var connection = _context.CreateConnection();
        var regions = await connection.QueryAsync<RegionDto>(query);
        return regions.ToList();
    }
    
    public async Task<RegionDto> GetById(int id)
    {
        const string query = RegionQuery.RegionByIdQuery;
        using var connection = _context.CreateConnection();
        var region = await connection.QuerySingleOrDefaultAsync<RegionDto>(query, new { Id = id });
        return region;
    }
    
    public async Task<RegionDto> GetByName(string name)
    {
        const string query = RegionQuery.RegionByNameQuery;
        using var connection = _context.CreateConnection();
        var region = await connection.QuerySingleOrDefaultAsync<RegionDto>(query, new { Description = name });
        return region;
    }
    
    public async Task<IEnumerable<RegionDto>> GetByTownId(int townId)
    {
        const string query = RegionQuery.AllTownRegionsQuery;
        using var connection = _context.CreateConnection();
        var regions = await connection.QueryAsync<RegionDto>(query, new { Id = townId });
        return regions.ToList();
    }
    
    public async Task<int> Create(RegionForManipulationDto regionDto)
    {
        const string query = RegionQuery.InsertRegionQuery;
        var param = new DynamicParameters(regionDto);
        
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
    
    public async Task Update(int id, RegionForManipulationDto regionDto)
    {
        const string query = RegionQuery.UpdateRegionQuery;
        var param = new DynamicParameters(regionDto);
        param.Add("Id", id);
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, param);
    }
    
    public async Task Delete(int id)
    {
        const string query = RegionQuery.DeleteRegionQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}