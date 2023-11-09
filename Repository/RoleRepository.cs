using Entities.Models;
using Interfaces;
using Microsoft.Extensions.Configuration;

namespace Repository;

internal sealed class RoleRepository : IRoleRepository
{
    private readonly IRepositoryManager _repository;
    private readonly IConfiguration _configuration;
    private readonly DapperContext _context;

    public RoleRepository(DapperContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Role>> GetUserRoles(int id)
    {
        throw new NotImplementedException();
    }

    public Task<UserRole> GetRole(string description)
    {
        throw new NotImplementedException();
    }
}