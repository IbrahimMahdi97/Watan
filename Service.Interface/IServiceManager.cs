﻿namespace Service.Interface;

public interface IServiceManager
{
    IUserService UserService { get; }
    IRoleService RoleService { get; }
    IPostService PostService { get; }
    IEventService EventService { get; }
    IComplaintService ComplaintService { get; }
    IProvinceService ProvinceService { get; }
    ITownService TownService { get; }
    IRegionService RegionService { get; }
    IEventAttendanceService EventAttendanceService { get; }
    IPostCommentService PostCommentService { get; }
    IPostLikeService PostLikeService { get; }
    INotificationService NotificationService { get; }
}