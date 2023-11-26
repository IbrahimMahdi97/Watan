namespace Shared.DataTransferObjects;

public class EventForManipulationDto
{
    public string? Type { get; set; } = null!;
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? LocationUrl { get; set; } = null!;
}