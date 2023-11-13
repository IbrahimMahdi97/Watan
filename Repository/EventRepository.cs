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
        var events = await connection.QueryAsync<EventWithPostDto>(query);
        return events.ToList();
    }

    public async Task<EventWithPostDto> GetEventById(int id)
    {
        const string query = EventQuery.EventByIdQuery;
        using var connection = _context.CreateConnection();
        var eventDetails = await connection.QuerySingleOrDefaultAsync<EventWithPostDto>(query, new { Id = id });
        return eventDetails;
    }

    public async Task<int> Create(EventWithPostDto eventDto, int userId)
    {
        const string insertPostQuery = PostQuery.InsertPostQuery;
        var postParams = new DynamicParameters(eventDto.PostDetails);
        postParams.Add("AddedByUserId", userId);
        postParams.Add("RecordDate", DateTime.Now);

        const string insertEventQuery = EventQuery.InsertEvent;
        var eventParams = new DynamicParameters(eventDto);
        eventParams.Add("Date", DateTime.Now);

        using var connection = _context.CreateConnection();
        connection.Open();

        using var trans = connection.BeginTransaction();
    
        try
        {
            var postId = await connection.ExecuteScalarAsync<int>(insertPostQuery, postParams, transaction: trans);
            if (postId <= 0)
            {
                trans.Rollback();
                return 0;
            }

            eventParams.Add("PostId", postId);
            await connection.ExecuteScalarAsync(insertEventQuery, eventParams, transaction: trans);

            trans.Commit();
            return postId;
        }
        catch (Exception ex)
        {
            trans.Rollback();
            throw;
        }
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