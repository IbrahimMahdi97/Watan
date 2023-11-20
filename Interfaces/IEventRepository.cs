using System.Data;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Interfaces;

public interface IEventRepository
{
    Task<PagedList<EventWithPostDto>> GetAllEvents(EventsParameters eventsParameters);
    Task<EventWithPostDto> GetEventById(int id);
    Task<int> Create(EventWithPostForCreationDto eventDto, int postId, IDbConnection connection, IDbTransaction transaction);
    Task Update(int id, EventWithPostForCreationDto eventDto);
}