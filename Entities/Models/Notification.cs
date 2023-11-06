namespace Entities.Models;

public class Notification
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsRead { get; set; }
    public int UserId { get; set; }
    public int AddedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RecordDate { get; set; }
}