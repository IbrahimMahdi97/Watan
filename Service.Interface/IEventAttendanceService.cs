namespace Service.Interface;

public interface IEventAttendanceService
{
    Task Create(int userId, int eventId);
}