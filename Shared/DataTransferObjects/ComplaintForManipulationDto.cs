using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects;

public class ComplaintForManipulationDto
{
    public int TypeId { get; set; }
    public string? Details { get; set; }
    public IFormFile? ComplaintImage { get; set; }
}