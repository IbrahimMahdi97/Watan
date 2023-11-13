using Shared.DataTransferObjects;

namespace Interfaces;

public interface IEventRepository
{
    Task<IEnumerable<EventWithPostDto>> GetAllEvents();
    Task<EventWithPostDto> GetEventById(int id);
    Task<int> Create(EventWithPostDto eventDto, int userId);
    Task Update(int id, EventForManiupulationDto eventDto);
}