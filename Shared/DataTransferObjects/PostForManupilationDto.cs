using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects;

public class PostForManipulationDto
{
    public string? Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public IFormFile? PostImage { get; set; }
}