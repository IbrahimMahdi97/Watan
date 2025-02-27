using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Interface;

public interface IEventService
{
    Task<PagedList<EventWithPostDto>> GetAllEvents(EventsParameters eventsParameters);
    Task<EventWithPostDto> GetEventById(int id, int userId);
    Task<int> Create(EventWithPostForCreationDto eventDto, int userId);
    Task Update(int id, EventWithPostForCreationDto eventDto);
    Task<IEnumerable<AttendeesCountDto>> GetAttendeesCountByProvinceIdAndTownId(int provinceId, int townId);
    Task<IEnumerable<EventDto>> GetFromDateToDate(DateTime fromDate, DateTime toDate);
}