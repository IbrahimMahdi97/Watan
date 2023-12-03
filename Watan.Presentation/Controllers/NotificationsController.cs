using System.Text.Json;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

namespace WatanPresentation.Controllers;

[Route("api/notifications")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IServiceManager _service;
    public NotificationsController(IServiceManager service) => _service = service;
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications([FromQuery] NotificationsParameters notificationsParameters)
    {
        var pagedResult = await _service.NotificationService.GetNotifications(notificationsParameters);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.MetaData));
        return Ok(pagedResult);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Notification>> SendNotification(NotificationForCreationDto notification)
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var notificationToReturn = await _service.NotificationService.SendNotification(notification, userId);
        return Ok(notificationToReturn);
    }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Notification>> GetNotification(int id)
    {
        var notificationToReturn = await _service.NotificationService.GetNotification(id);
        return Ok(notificationToReturn);
    }
    
    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateNotificationIsRead(int id)
    {
        await _service.NotificationService.UpdateNotificationIsRead(id);
        return NoContent();
    }
    
    [Authorize]
    [HttpGet("new-notifications-count")]
    public async Task<ActionResult<int>> GetNewNotificationsCount()
    {
        var userId = User.RetrieveUserIdFromPrincipal();
        var count = await _service.NotificationService.GetNewNotificationsCount(userId);
        return Ok(count);
    }

}