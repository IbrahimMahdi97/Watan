using Dapper;
using Entities.Models;
using Interfaces;
using Repository.Query;

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

    public Task<UserRole> GetRole(string description)
    {
        throw new NotImplementedException();
    }
}