using Entities.Exceptions;
using FirebaseAdmin.Messaging;
using Interfaces;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Notification = Entities.Models.Notification;

namespace Service;

public class NotificationService : INotificationService
{
    private readonly IRepositoryManager _repository;
    private readonly IFirebaseService _firebaseService;
    private readonly IUserService _userService;

    public NotificationService(IRepositoryManager repository, IFirebaseService firebaseService,
        IUserService userService)
    {
        _repository = repository;
        _firebaseService = firebaseService;
        _userService = userService;
    }

    public async Task<PagedList<Notification>> GetNotifications(NotificationsParameters notificationsParameters,
        int userId)
    {
        var notifications = await _repository.Notification.GetNotifications(notificationsParameters, userId);
        return notifications;
    }

    public async Task<int> GetNewNotificationsCount(int userId)
    {
        var count = await _repository.Notification.GetNewNotificationsCount(userId);
        return count;
    }

    public async Task SendNotifications(UsersParameters parameters, NotificationForCreationDto notification, int userId)
    {
        var users = await _userService.GetByParameters(parameters, false);
        foreach (var user in users)
        {
            notification.UserId = user.Id;
            await SendNotification(notification, userId);
        }
    }

    public async Task<Notification?> SendNotification(NotificationForCreationDto notification, int userId)
    {
        if (notification.Title is { Length: > 50 })
            throw new StringLimitExceededBadRequestException("Title", 50);

        var insertedId = await _repository.Notification.AddNotification(new Notification()
        {
            Title = notification.Title,
            Description = notification.Description,
            UserId = notification.UserId,
            AddedByUserId = userId
        });

        string? deviceId = null;

        if (notification.UserId > 0)
        {
            deviceId = await _repository.User.GetUserDeviceId(notification.UserId);
            if (string.IsNullOrEmpty(deviceId))
                throw new DeviceIdNotFoundException(notification.UserId);
        }

        var message = new Message()
        {
            Token = deviceId ?? "/topics/all",
            Data = new Dictionary<string, string>
            {
                ["notificationId"] = insertedId.ToString(),
                ["userId"] = notification.UserId.ToString(),
                ["date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            },
            Notification = new FirebaseAdmin.Messaging.Notification()
            {
                Title = notification.Title,
                Body = notification.Description
            },
            Android = new AndroidConfig
            {
                Notification = new AndroidNotification
                {
                    Sound = "default"
                }
            },
            Apns = new ApnsConfig
            {
                Aps = new Aps
                {
                    Sound = "default"
                }
            }
        };

        await _firebaseService.SendNotificationAsync(message);

        return new Notification()
        {
            Id = insertedId,
            Title = notification.Title,
            Description = notification.Description,
            AddedByUserId = userId,
            IsRead = false,
            UserId = notification.UserId,
            RecordDate = DateTime.Now
        };
    }

    public async Task<Notification?> GetNotification(int id)
    {
        var notification = await _repository.Notification.GetNotificationById(id);

        if (notification is not null && !notification.IsRead)
            await _repository.Notification.UpdateNotificationIsRead(id);

        return notification;
    }

    public async Task UpdateNotificationIsRead(int id)
    {
        await _repository.Notification.UpdateNotificationIsRead(id);
    }
}