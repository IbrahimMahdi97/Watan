using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/user_regions")]
[ApiController]
public class UserRegionsController : ControllerBase
{
    private readonly IServiceManager _service;
    public UserRegionsController(IServiceManager service) => _service = service;
}