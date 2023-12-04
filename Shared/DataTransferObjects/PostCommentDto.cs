namespace Shared.DataTransferObjects;

public class PostCommentDto : PostCommentForManiupulationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime RecordDate { get; set; }
    public string? FullName { get; set; }
    public IEnumerable<LikeDto>? Likes { get; set; }
}