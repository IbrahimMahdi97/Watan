using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IRoleService
{
    Task<IEnumerable<UserRoleDto>> GetAll();
}