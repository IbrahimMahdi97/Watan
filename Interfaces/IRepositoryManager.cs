namespace Interfaces;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    IRoleRepository Role { get; }
    IPostRepository Post { get; }
    IEventRepository Event { get; }
    IComplaintRepository Complaint { get; }
}