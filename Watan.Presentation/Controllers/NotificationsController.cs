using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/notifications")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly IServiceManager _service;
    public NotificationsController(IServiceManager service) => _service = service;
}