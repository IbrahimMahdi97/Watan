using Interfaces;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IRoleRepository> _roleRepository;

    public RepositoryManager(DapperContext dapperContext)
    {
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(dapperContext));
        _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(dapperContext));
    }
    public IUserRepository User => _userRepository.Value;
    public IRoleRepository Role => _roleRepository.Value;
}