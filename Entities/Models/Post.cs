namespace Entities.Models;

public class Post
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int TypeId { get; set; }
    public int AddedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RecordDate { get; set; }
}