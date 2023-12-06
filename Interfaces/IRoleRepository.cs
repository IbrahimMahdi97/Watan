using Entities.Models;
using Shared.DataTransferObjects;

namespace Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetUserRoles(int id);
    Task<IEnumerable<UserRoleDto>> GetAll();
}