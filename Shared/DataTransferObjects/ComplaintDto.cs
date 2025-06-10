using Entities.Enums;

namespace Shared.DataTransferObjects;

public class ComplaintDto : ComplaintForManipulationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime RecordDate { get; set; }
    public ComplaintStatus Status { get; set; }
    public string? ComplaintType { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}