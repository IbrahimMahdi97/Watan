namespace Entities.Models;

public class EventDetails
{
    public int PostId { get; set; }
    public string? Type { get; set; }
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }  
    public string? LocationUrl { get; set; }
}