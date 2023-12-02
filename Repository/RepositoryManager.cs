using Interfaces;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IUserRepository> _userRepository;
    private readonly Lazy<IRoleRepository> _roleRepository;
    private readonly Lazy<IPostRepository> _postRepository;
    private readonly Lazy<IEventRepository> _eventRepository;
    private readonly Lazy<IComplaintRepository> _complaintRepository;
    private readonly Lazy<IProvinceRepository> _provinceRepository;
    private readonly Lazy<ITownRepository> _townRepository;
    private readonly Lazy<IRegionRepository> _regionRepository;
    private readonly Lazy<IEventAttendanceRepository> _eventAttendanceRepository;
    private readonly Lazy<IPostCommentRepository> _postCommentRepository;
    private readonly Lazy<IPostLikeRepository> _postLikeRepository;
    private readonly Lazy<INotificationRepository> _notificationRepository;
    public RepositoryManager(DapperContext dapperContext)
    {
        _userRepository = new Lazy<IUserRepository>(() => new UserRepository(dapperContext));
        _roleRepository = new Lazy<IRoleRepository>(() => new RoleRepository(dapperContext));
        _postRepository = new Lazy<IPostRepository>(() => new PostRepository(dapperContext));
        _eventRepository = new Lazy<IEventRepository>(() => new EventRepository(dapperContext));
        _complaintRepository = new Lazy<IComplaintRepository>(() => new ComplaintRepository(dapperContext));
        _provinceRepository = new Lazy<IProvinceRepository>(() => new ProvinceRepository(dapperContext));
        _townRepository = new Lazy<ITownRepository>(() => new TownRepository(dapperContext));
        _regionRepository = new Lazy<IRegionRepository>(() => new RegionRepository(dapperContext));
        _eventAttendanceRepository = new Lazy<IEventAttendanceRepository>(
            () => new EventAttendanceRepository(dapperContext));
        _postCommentRepository = new Lazy<IPostCommentRepository>(
            () => new PostCommentRepository(dapperContext));
        _postLikeRepository = new Lazy<IPostLikeRepository>(() => new PostLikeRepository(dapperContext));
        _notificationRepository = new Lazy<INotificationRepository>(() => new NotificationRepository(dapperContext));
    }
    public IUserRepository User => _userRepository.Value;
    public IRoleRepository Role => _roleRepository.Value;
    public IPostRepository Post => _postRepository.Value;
    public IEventRepository Event => _eventRepository.Value;
    public IComplaintRepository Complaint => _complaintRepository.Value;
    public IProvinceRepository Province  => _provinceRepository.Value;
    public ITownRepository Town => _townRepository.Value;
    public IRegionRepository Region => _regionRepository.Value;
    public IEventAttendanceRepository EventAttendance => _eventAttendanceRepository.Value;
    public IPostCommentRepository PostComment => _postCommentRepository.Value;
    public IPostLikeRepository PostLike => _postLikeRepository.Value;
    public INotificationRepository Notification => _notificationRepository.Value;
}