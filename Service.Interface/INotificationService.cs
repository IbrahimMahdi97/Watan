using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Interface;

public interface INotificationService
{
    Task<PagedList<Notification>> GetNotifications(NotificationsParameters notificationsParameters, int userId);
    Task<Notification?> SendNotification(NotificationForCreationDto notification, int userId);
    Task<Notification?> GetNotification(int id);
    Task UpdateNotificationIsRead(int id);
    Task<int> GetNewNotificationsCount(int userId);
}