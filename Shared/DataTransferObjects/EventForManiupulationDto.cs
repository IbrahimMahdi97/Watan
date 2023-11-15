using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects;

public class EventForManiupulationDto
{
    public string? Type { get; set; } = null!;
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string? LocationUrl { get; set; } = null!;
}