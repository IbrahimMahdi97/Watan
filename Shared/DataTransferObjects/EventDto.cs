namespace Shared.DataTransferObjects;

public class EventDto
{
    public int PostId { get; set; }
    public string Title { get; set; } = null!;
    public string Province { get; set; } = null!;
    public float AttendancePercentage { get; set; }
    public int AttendanceCount { get; set; }
}