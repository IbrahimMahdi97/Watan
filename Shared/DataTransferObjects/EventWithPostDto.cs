namespace Shared.DataTransferObjects;

public class EventWithPostDto : EventForManipulationDto
{
    public string Province { get; set; } = null!;
    public string Town { get; set; } = null!;
    public PostDto PostDetails { get; set; } = null!;
}