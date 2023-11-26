namespace Shared.DataTransferObjects;

public class EventWithPostDto : EventForManipulationDto
{
    public PostDto PostDetails { get; set; } = null!;
}