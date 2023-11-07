namespace Entities.Models;

public class PostComment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string? Comment { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RecordDate { get; set; }
}