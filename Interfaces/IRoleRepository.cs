using Entities.Models;
namespace Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetUserRoles(int id);
    Task<UserRole> GetRole(string description);
}