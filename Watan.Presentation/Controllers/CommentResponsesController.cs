using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace WatanPresentation.Controllers;

[Route("api/comment_responses")]
[ApiController]
public class CommentResponsesController : ControllerBase
{
    private readonly IServiceManager _service;
    public CommentResponsesController(IServiceManager service) => _service = service;
}