namespace Shared.DataTransferObjects;

public class PostDto : PostForManipulationDto
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
}