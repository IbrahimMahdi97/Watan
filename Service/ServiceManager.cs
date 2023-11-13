using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IRoleRepository> _roleRepository;
    private readonly Lazy<IPostService> _postService;
    private readonly Lazy<IEventService> _eventService;
    

    public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration)
    {
        _userService = new Lazy<IUserService>(() => 
            new UserService(repositoryManager, configuration));
        _postService = new Lazy<IPostService>(() =>
            new PostService(repositoryManager, configuration));
        _eventService = new Lazy<IEventService>(() =>
            new EventService(repositoryManager, configuration));
    }

    public IUserService UserService => _userService.Value;
    public IRoleRepository Role => _roleRepository.Value;
    public IPostService PostService => _postService.Value;
    public IEventService EventService => _eventService.Value;
}