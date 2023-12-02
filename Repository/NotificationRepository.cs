using System.Data;
using Dapper;
using Entities.Models;
using Interfaces;
using Repository.Query;
using Shared.RequestFeatures;

namespace Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly DapperContext _context;

    public NotificationRepository(DapperContext context) => _context = context;

    public async Task<PagedList<Notification>> GetNotifications(NotificationsParameters notificationsParameters)
    {
        var skip = (notificationsParameters.PageNumber - 1) * notificationsParameters.PageSize;
        const string countQuery = NotificationQuery.SelectCountQuery;
        const string query = NotificationQuery.SelectByParametersQuery;

        var param = new DynamicParameters();
        param.Add("skip", skip, DbType.Int32);
        param.Add("take", notificationsParameters.PageSize, DbType.Int32);
        param.Add("userId", notificationsParameters.UserId, DbType.Int32);

        using var connection = _context.CreateConnection();
        var count = await connection.QueryFirstOrDefaultAsync<int>(countQuery, param);
        var notifications = (await connection.QueryAsync<Notification>(query, param)).ToList();

        return new PagedList<Notification>(notifications, count, notificationsParameters.PageNumber,
            notificationsParameters.PageSize);
    }

    public async Task<int> GetNewNotificationsCount(int userId)
    {
        const string countQuery = NotificationQuery.SelectNewCountQuery;
        using var connection = _context.CreateConnection();
        var count = await connection.QueryFirstOrDefaultAsync<int>(countQuery, new { Id = userId });
        return count;
    }

    public async Task<int> AddNotification(Notification notification)
    {
        const string query = NotificationQuery.InsertQuery;
        var param = new DynamicParameters(notification);

        using var connection = _context.CreateConnection();
        connection.Open();

        using var trans = connection.BeginTransaction();
        var id = await connection.ExecuteScalarAsync<int>(query, param, transaction: trans);

        trans.Commit();

        return id;
    }

    public async Task<Notification?> GetNotificationById(int id)
    {
        const string query = NotificationQuery.SelectByIdQuery;
        using var connection = _context.CreateConnection();
        var result = await connection.QueryAsync<Notification>(query, new { Id = id });
        return result.FirstOrDefault();
    }

    public async Task UpdateNotificationIsRead(int id)
    {
        const string query = NotificationQuery.UpdateIsReadQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }

}