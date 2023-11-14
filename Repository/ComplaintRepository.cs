using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

public class ComplaintRepository : IComplaintRepository
{
    private readonly DapperContext _context;

    public ComplaintRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ComplaintDto>> GetAllComplaints()
    {
        const string query = ComplaintQuery.AllComplaintsQuery;
        using var connection = _context.CreateConnection();
        var complaints = await connection.QueryAsync<ComplaintDto>(query);
        return complaints.ToList();
    }
    
    public async Task<ComplaintDto> GetComplaintById(int id)
    {
        const string query = ComplaintQuery.ComplaintByIdQuery;
        using var connection = _context.CreateConnection();
        var complaint = await connection.QuerySingleOrDefaultAsync<ComplaintDto>(query, new { Id = id });
        return complaint;
    }

    public async Task<int> CreateComplaint(ComplaintForManipulationDto complaintDto, int userId)
    {
        const string query = ComplaintQuery.InsertComplaintQuery;
        var param = new DynamicParameters(complaintDto);
        param.Add("UserId", userId);
        param.Add("RecordDate", DateTime.Now);
        
        using var connection = _context.CreateConnection();
        connection.Open();
        using var trans = connection.BeginTransaction();
        var id = await connection.QuerySingleAsync<int>(query, param, transaction: trans);
        if (id <= 0)
        {
            trans.Rollback();
            return 0;
        }
        trans.Commit();
        return id;
    }
    public async Task UpdateComplaint(int id, ComplaintForManipulationDto complaintDto)
    {
        const string query = ComplaintQuery.UpdateComplaintQuery;
        var param = new DynamicParameters(complaintDto);
        param.Add("Id", id);
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, param);
    }
    public async Task DeleteComplaint(int id)
    {
        const string query = ComplaintQuery.DeleteComplaintQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}