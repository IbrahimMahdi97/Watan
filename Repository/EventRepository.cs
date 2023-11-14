using System.Data;
using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

public class EventRepository : IEventRepository
{
    private readonly DapperContext _context;

    public EventRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EventWithPostDto>> GetAllEvents()
    {
        const string query = EventQuery.AllEventsQuery;
        using var connection = _context.CreateConnection();
        var events = await connection.QueryAsync<EventForManiupulationDto, PostForManipulationDto, EventWithPostDto>(
            query,
            (eventDetails, postDetails) =>
            {
                var eventWithPost = new EventWithPostDto
                {
                    Type = eventDetails.Type,
                    ProvinceId = eventDetails.ProvinceId,
                    TownId = eventDetails.TownId,
                    StartTime = eventDetails.StartTime,
                    EndTime = eventDetails.EndTime,
                    LocationUrl = eventDetails.LocationUrl,
                    PostDetails = postDetails
                };
                return eventWithPost;
            },
            splitOn: "Id"
        );
        return events.ToList();
    }

    public async Task<EventWithPostDto> GetEventById(int id)
    {
        const string query = EventQuery.EventByIdQuery;
        using var connection = _context.CreateConnection();
        var eventDetails = await connection.QuerySingleOrDefaultAsync<EventWithPostDto>(query, new { Id = id });
        return eventDetails;
    }

    public async Task<int> Create(EventWithPostDto eventDto, int postId, IDbConnection connection, IDbTransaction transaction)
    {
        const string insertEventQuery = EventQuery.InsertEvent;
        var eventParams = new DynamicParameters(eventDto);
        eventParams.Add("Date", DateTime.Now);
        eventParams.Add("PostId", postId);

        await connection.ExecuteAsync(insertEventQuery, eventParams, transaction: transaction);

        transaction.Commit();
        connection.Close();
        return postId;
    }

    public async Task Update(int id, EventForManiupulationDto eventDto)
    {
        const string query = EventQuery.UpdateEventQuery;
        var param = new DynamicParameters(eventDto);
        param.Add("Id", id);
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, param);
    }
}