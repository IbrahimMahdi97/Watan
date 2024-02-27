namespace Shared.DataTransferObjects;

public class AttendeesCountDto
{
    public DateTime StartOfWeek { get; set; }
    public int CountOfInside { get; set; }
    public int CountOfOutside { get; set; }
}