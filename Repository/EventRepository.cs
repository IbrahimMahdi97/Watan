using System.Data;
using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Repository;

public class EventRepository : IEventRepository
{
    private readonly DapperContext _context;

    public EventRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<PagedList<EventWithPostDto>> GetAllEvents(EventsParameters eventsParameters)
    {
        const string query = EventQuery.SelectEventsByParametersQuery;
        const string countQuery = EventQuery.SelectCountOfEventsByParametersQuery;

        var skip = (eventsParameters.PageNumber - 1) * eventsParameters.PageSize;
        var param = new DynamicParameters(eventsParameters);
        param.Add("skip", skip);

        using var connection = _context.CreateConnection();

        var count = await connection.QueryFirstOrDefaultAsync<int>(countQuery, param);
        var events = await connection.QueryAsync<EventWithPostDto>(query, param: param);

        return new PagedList<EventWithPostDto>(events, count, eventsParameters.PageNumber, eventsParameters.PageSize);
    }

    public async Task<EventWithPostDto> GetEventById(int id)
    {
        const string query = EventQuery.EventByIdQuery;
        using var connection = _context.CreateConnection();
        var eventDetails = await connection.QuerySingleOrDefaultAsync<EventWithPostDto>(query, new { Id = id });
        return eventDetails;
    }

    public async Task<int> Create(EventWithPostForCreationDto eventDto, int postId, IDbConnection connection,
        IDbTransaction transaction)
    {
        const string insertEventQuery = EventQuery.InsertEvent;
        var eventParams = new DynamicParameters(eventDto);
        eventParams.Add("PostId", postId);

        await connection.ExecuteAsync(insertEventQuery, eventParams, transaction: transaction);

        transaction.Commit();
        connection.Close();
        return postId;
    }

    public async Task Update(int id, EventWithPostForCreationDto eventDto)
    {
        const string query = EventQuery.UpdateEventQuery;
        var param = new DynamicParameters(eventDto);
        param.Add("Id", id);
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, param);
    }

    public async Task<IEnumerable<AttendeesCountDto>> GetAttendeesCountByProvinceIdAndTownId(int provinceId, int townId)
    {
        const string query = EventQuery.SelectAttendeesCountByProvinceIdAndTownIdQuery;
        using var connection = _context.CreateConnection();
        connection.Open();
        return await connection.QueryAsync<AttendeesCountDto>(query,
            new { ProvinceId = provinceId, TownId = townId });
    }

    public async Task<IEnumerable<EventDto>> GetFromDateToDate(DateTime fromDate, DateTime toDate)
    {
        const string query = EventQuery.SelectFromDateToDateQuery;
        using var connection = _context.CreateConnection();
        connection.Open();
        return await connection.QueryAsync<EventDto>(query, new { FromDate = fromDate, ToDate = toDate });
    }
}