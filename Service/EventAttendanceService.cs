using Interfaces;
using Service.Interface;

namespace Service;

internal sealed class EventAttendanceService : IEventAttendanceService
{
    private readonly IRepositoryManager _repository;
    public EventAttendanceService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task Create(int userId, int eventId)
    {
        await _repository.EventAttendance.Create(userId, eventId);
    }

}