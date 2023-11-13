namespace Shared.DataTransferObjects;

public class EventWithPostDto : EventForManiupulationDto
{
    public PostForManipulationDto PostDetails { get; set; } = null!;
}