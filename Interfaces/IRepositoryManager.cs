namespace Interfaces;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    IRoleRepository Role { get; }
}