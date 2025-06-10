using Dapper;
using Entities.Models;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

internal sealed class RoleRepository : IRoleRepository
{
    private readonly DapperContext _context;

    public RoleRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetUserRoles(int id)
    {
        const string query = RoleQuery.UserRolesByIdQuery;
        using var connection = _context.CreateConnection();
        var roles = await connection.QueryAsync<Role>(query, new { Id = id });
        return roles;
    }

    public async Task<IEnumerable<UserRoleDto>> GetAll()
    {
        const string query = RoleQuery.AllQuery;
        using var connection = _context.CreateConnection();
        var roles = await connection.QueryAsync<UserRoleDto>(query);
        return roles;
    }
}