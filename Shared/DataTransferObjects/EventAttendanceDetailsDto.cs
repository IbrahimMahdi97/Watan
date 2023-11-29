namespace Shared.DataTransferObjects;

public class EventAttendanceDetailsDto
{
    public int Count { get; set; }
    public IEnumerable<UserForManipulationDto>? Attendees { get; set; }
}