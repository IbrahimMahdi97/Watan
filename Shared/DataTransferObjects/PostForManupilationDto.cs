namespace Shared.DataTransferObjects;

public class PostForManipulationDto
{
    public string? Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public int TypeId { get; set; }
}