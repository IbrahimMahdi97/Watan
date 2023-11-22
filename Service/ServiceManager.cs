﻿using Interfaces;
using Microsoft.Extensions.Configuration;
using Service.Interface;

namespace Service;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IUserService> _userService;
    private readonly Lazy<IPostService> _postService;
    private readonly Lazy<IEventService> _eventService;
    private readonly Lazy<IComplaintService> _complaintService;
    

    public ServiceManager(IRepositoryManager repositoryManager, IConfiguration configuration)
    {
        Lazy<IFileStorageService> fileStorageService = new(() =>
            new FileStorageService());
        
        _userService = new Lazy<IUserService>(() => 
            new UserService(repositoryManager, fileStorageService.Value, configuration));
        _postService = new Lazy<IPostService>(() =>
            new PostService(repositoryManager, fileStorageService.Value, configuration));
        _eventService = new Lazy<IEventService>(() =>
            new EventService(repositoryManager, fileStorageService.Value, configuration));
        _complaintService = new Lazy<IComplaintService>(() =>
            new ComplaintService(repositoryManager, fileStorageService.Value, configuration));
    }

    public IUserService UserService => _userService.Value;
    public IPostService PostService => _postService.Value;
    public IEventService EventService => _eventService.Value;
    public IComplaintService ComplaintService => _complaintService.Value;
}