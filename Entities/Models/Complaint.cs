namespace Entities.Models;

public class Complaint
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TypeId { get; set; }
    public string? Details { get; set; }
    public int Status { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RecordDate { get; set; }
}