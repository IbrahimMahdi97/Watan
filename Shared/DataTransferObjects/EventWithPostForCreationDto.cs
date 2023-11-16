namespace Shared.DataTransferObjects;

public class EventWithPostForCreationDto : EventForManiupulationDto
{
    public PostForManipulationDto PostDetails { get; set; } = null!;
}