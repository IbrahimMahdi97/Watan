using System.Data;
using Shared.DataTransferObjects;

namespace Interfaces;

public interface IEventRepository
{
    Task<IEnumerable<EventWithPostDto>> GetAllEvents();
    Task<EventWithPostDto> GetEventById(int id);
    Task<int> Create(EventWithPostDto eventDto, int postId, IDbConnection connection, IDbTransaction transaction);
    Task Update(int id, EventForManiupulationDto eventDto);
}