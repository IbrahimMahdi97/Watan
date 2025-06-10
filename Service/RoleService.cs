using Interfaces;
using Service.Interface;
using Shared.DataTransferObjects;

namespace Service;

public class RoleService : IRoleService
{
    private readonly IRepositoryManager _repository;
    
    public RoleService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserRoleDto>> GetAll()
    {
        var roles = await _repository.Role.GetAll();
        return roles;
    }
}