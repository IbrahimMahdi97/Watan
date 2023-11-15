using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IEventService
{
    Task<IEnumerable<EventWithPostDto>> GetAllEvents();
    Task<EventWithPostDto> GetEventById(int id);
    Task<int> Create(EventWithPostDto eventDto, int userId);
    Task Update(int id, EventForManiupulationDto eventDto);
}