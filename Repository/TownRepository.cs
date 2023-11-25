using Interfaces;
using Shared.DataTransferObjects;
using Repository.Query;
using Dapper;

namespace Repository;

public class TownRepository : ITownRepository
{
    private readonly DapperContext _context;

    public TownRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<TownDto>> GetAll()
    {
        const string query = TownQuery.AllTownsQuery;
        using var connection = _context.CreateConnection();
        var towns = await connection.QueryAsync<TownDto>(query);
        return towns.ToList();
    }
    
    public async Task<TownDto> GetById(int id)
    {
        const string query = TownQuery.TownByIdQuery;
        using var connection = _context.CreateConnection();
        var town = await connection.QuerySingleOrDefaultAsync<TownDto>(query, new { Id = id });
        return town;
    }
    
    public async Task<TownDto> GetByName(string name)
    {
        const string query = TownQuery.TownByNameQuery;
        using var connection = _context.CreateConnection();
        var town = await connection.QuerySingleOrDefaultAsync<TownDto>(query, new { Description = name });
        return town;
    }
    
    public async Task<IEnumerable<TownDto>> GetByProvinceId(int provinceId)
    {
        const string query = TownQuery.AllProvinceTownsQuery;
        using var connection = _context.CreateConnection();
        var towns = await connection.QueryAsync<TownDto>(query, new { Id = provinceId });
        return towns.ToList();
    }
    
    public async Task<int> Create(TownForManipulationDto townDto)
    {
        const string query = TownQuery.InsertTownQuery;
        var param = new DynamicParameters(townDto);
        
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
    
    public async Task Update(int id, TownForManipulationDto townDto)
    {
        const string query = TownQuery.UpdateTownQuery;
        var param = new DynamicParameters(townDto);
        param.Add("Id", id);
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, param);
    }
    
    public async Task Delete(int id)
    {
        const string query = TownQuery.DeleteTownQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}