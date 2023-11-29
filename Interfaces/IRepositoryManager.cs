namespace Interfaces;

public interface IRepositoryManager
{
    IUserRepository User { get; }
    IRoleRepository Role { get; }
    IPostRepository Post { get; }
    IEventRepository Event { get; }
    IComplaintRepository Complaint { get; }
    IProvinceRepository Province { get; }
    ITownRepository Town { get; }
    IRegionRepository Region { get; }
    IEventAttendanceRepository EventAttendance { get; }
    IPostCommentRepository PostComment { get; }
    IPostLikeRepository PostLike { get; }
}