using Entities.Models;
using Shared.RequestFeatures;

namespace Interfaces;

public interface INotificationRepository
{
    Task<PagedList<Notification>> GetNotifications(NotificationsParameters notificationsParameters);
    Task<int> GetNewNotificationsCount(int userId);
    Task<int> AddNotification(Notification notification);
    Task<Notification?> GetNotificationById(int id);
    Task UpdateNotificationIsRead(int id);
}