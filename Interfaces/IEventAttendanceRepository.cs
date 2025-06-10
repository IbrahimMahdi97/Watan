namespace Interfaces;

public interface IEventAttendanceRepository
{
    Task Create(int userId, int eventId);
}