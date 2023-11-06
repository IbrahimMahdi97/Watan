namespace Entities.Models;

public class CommentResponse
{
    public int CommentId { get; set; }
    public int UserId { get; set; }
    public string? Response { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RecordDate { get; set; }
}