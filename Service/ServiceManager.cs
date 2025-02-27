﻿using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IRoleService> _roleService;
    private readonly Lazy<IPostService> _postService;
    private readonly Lazy<IEventService> _eventService;
    private readonly Lazy<IComplaintService> _complaintService;
    private readonly Lazy<IProvinceService> _provinceService;
    private readonly Lazy<ITownService> _townService;
    private readonly Lazy<IRegionService> _regionService;
    private readonly Lazy<IEventAttendanceService> _eventAttendanceService;
    private readonly Lazy<IPostCommentService> _postCommentService;
    private readonly Lazy<IPostLikeService> _postLikeService;
    private readonly Lazy<INotificationService> _notificationService;

    public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration)
    {
        Lazy<IFileStorageService> fileStorageService = new(() =>
            new FileStorageService());

        Lazy<IFirebaseService> firebaseService = new(() =>
            new FirebaseService());

        _userService = new Lazy<IUserService>(() =>
            new UserService(repositoryManager, fileStorageService.Value, configuration));
        _roleService = new Lazy<IRoleService>(() => new RoleService(repositoryManager));
        _postService = new Lazy<IPostService>(() =>
            new PostService(repositoryManager, fileStorageService.Value, configuration));
        _eventService = new Lazy<IEventService>(() =>
            new EventService(repositoryManager, fileStorageService.Value, configuration));
        _complaintService = new Lazy<IComplaintService>(() =>
            new ComplaintService(repositoryManager, fileStorageService.Value, configuration));
        _provinceService = new Lazy<IProvinceService>(() => new ProvinceService(repositoryManager));
        _townService = new Lazy<ITownService>(() => new TownService(repositoryManager));
        _regionService = new Lazy<IRegionService>(() => new RegionService(repositoryManager));
        _eventAttendanceService = new Lazy<IEventAttendanceService>(() =>
            new EventAttendanceService(repositoryManager));
        _postCommentService = new Lazy<IPostCommentService>(() => new PostCommentService(repositoryManager,
            fileStorageService.Value, configuration));
        _postLikeService = new Lazy<IPostLikeService>(() => new PostLikeService(repositoryManager));
        _notificationService = new Lazy<INotificationService>(() =>
            new NotificationService(repositoryManager, firebaseService.Value, _userService.Value));
    }

    public IUserService UserService => _userService.Value;
    public IRoleService RoleService => _roleService.Value;
    public IPostService PostService => _postService.Value;
    public IEventService EventService => _eventService.Value;
    public IComplaintService ComplaintService => _complaintService.Value;
    public IProvinceService ProvinceService => _provinceService.Value;
    public ITownService TownService => _townService.Value;
    public IRegionService RegionService => _regionService.Value;
    public IEventAttendanceService EventAttendanceService => _eventAttendanceService.Value;
    public IPostCommentService PostCommentService => _postCommentService.Value;
    public IPostLikeService PostLikeService => _postLikeService.Value;
    public INotificationService NotificationService => _notificationService.Value;
}