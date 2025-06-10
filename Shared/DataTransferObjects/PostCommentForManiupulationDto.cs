namespace Shared.DataTransferObjects;

public class PostCommentForManiupulationDto
{
    public int PostId { get; set; }
    public string? Comment { get; set; } = null!;
    public int? ParentCommentId { get; set; }
}