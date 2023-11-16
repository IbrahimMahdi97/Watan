namespace Shared.DataTransferObjects;

public class EventWithPostDto : EventForManiupulationDto
{
    public PostDto PostDetails { get; set; } = null!;
}