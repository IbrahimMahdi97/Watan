namespace Service.Interface;

public interface IServiceManager
{
    IUserService UserService { get; }
    IPostService PostService { get; }
    IEventService EventService { get; }
    IComplaintService ComplaintService { get; }
}