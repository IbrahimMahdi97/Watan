using Interfaces;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IRoleRepository> _roleRepository;
    private readonly Lazy<IPostRepository> _postRepository;
    private readonly Lazy<IEventRepository> _eventRepository;
    private readonly Lazy<IComplaintRepository> _complaintRepository;
    public RepositoryManager(DapperContext dapperContext)
    {
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(dapperContext));
        _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(dapperContext));
        _postRepository = new Lazy<IPostRepository>(() => new PostRepository(dapperContext));
        _eventRepository = new Lazy<IEventRepository>(() => new EventRepository(dapperContext));
        _complaintRepository = new Lazy<IComplaintRepository>(() => new ComplaintRepository(dapperContext));
    }
    public IUserRepository User => _userRepository.Value;
    public IRoleRepository Role => _roleRepository.Value;
    public IPostRepository Post => _postRepository.Value;
    public IEventRepository Event => _eventRepository.Value;
    public IComplaintRepository Complaint => _complaintRepository.Value;
}