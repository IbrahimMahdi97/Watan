namespace Shared.DataTransferObjects;

public class MyComplaintsDto
{
    public int OnHoldComplaintsCount { get; set; }
    public int CancelledComplaintsCount { get; set; }
    public int DoneComplaintsCount { get; set; }
    public IEnumerable<ComplaintDto> Complaints { get; set; } = Array.Empty<ComplaintDto>();
}