namespace Shared.DataTransferObjects;

public class EventWithPostForCreationDto : EventForManipulationDto
{
    public PostForManipulationDto PostDetails { get; set; } = null!;
}