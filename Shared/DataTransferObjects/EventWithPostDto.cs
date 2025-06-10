namespace Shared.DataTransferObjects;

public class EventWithPostDto : EventForManipulationDto
{
    public string Province { get; set; } = null!;
    public string Town { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime RecordDate { get; set; }
    public int Id { get; set; }
    public PostDto PostDetails { get; set; } = null!;
    public int AttendanceCount { get; set; }
}