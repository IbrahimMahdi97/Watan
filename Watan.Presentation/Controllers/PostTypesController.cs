using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/post_types")]
[ApiController]
public class PostTypesController : ControllerBase
{
    private readonly IServiceManager _service;
    public PostTypesController(IServiceManager service) => _service = service;
}