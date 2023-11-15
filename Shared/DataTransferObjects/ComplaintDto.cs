namespace Shared.DataTransferObjects;

public class ComplaintDto : ComplaintForManipulationDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RecordDate { get; set; }
}