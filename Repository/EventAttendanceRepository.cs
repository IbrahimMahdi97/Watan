using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

public class EventAttendanceRepository : IEventAttendanceRepository
{
    private readonly DapperContext _context;

    public EventAttendanceRepository(DapperContext context)
    {
        _context = context;
    }

    // public async Task<EventAttendanceDetailsDto> GetAll(int eventId)
    // {
    //     const string query = EventAttendanceQuery.AttendeesDetailQuery;
    //     var param = new DynamicParameters();
    //     param.Add("PostId", eventId);
    //     using var connection = _context.CreateConnection();
    //     var attendees = await connection.QueryAsync<EventAttendanceDetailsDto>(query, 
    //         param);
    //     return attendees;
    // }

    public async Task Create(int userId, int eventId)
    {
        const string query = EventAttendanceQuery.InsertQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new {PostId = eventId, UserId = userId});
    }
}