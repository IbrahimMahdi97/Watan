using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Repository;

public class ComplaintRepository : IComplaintRepository
{
    private readonly DapperContext _context;

    public ComplaintRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<ComplaintDto>> GetAllComplaints(ComplaintsParameters parameters)
    {
        const string query = ComplaintQuery.ComplaintsByParametersQuery;
        const string countQuery = ComplaintQuery.ComplaintsCountByParametersQuery;

        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var param = new DynamicParameters(parameters);
        param.Add("skip", skip);
        param.Add("UserId", 0);

        using var connection = _context.CreateConnection();

        var count = await connection.QueryFirstOrDefaultAsync<int>(countQuery, param);
        var complaints = await connection.QueryAsync<ComplaintDto>(query, param: param);

        return new PagedList<ComplaintDto>(complaints, count, parameters.PageNumber, parameters.PageSize);
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
    public async Task UpdateComplaint(int id, ComplaintForUpdateDto complaintDto)
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

    public async Task<IEnumerable<ComplaintDto>> GetUserComplaints(int userId, ComplaintsParameters parameters)
    {
        const string query = ComplaintQuery.ComplaintsByParametersQuery;
        const string countQuery = ComplaintQuery.ComplaintsCountByParametersQuery;

        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var param = new DynamicParameters(parameters);
        param.Add("skip", skip);
        param.Add("UserId", userId);

        using var connection = _context.CreateConnection();

        var count = await connection.QueryFirstOrDefaultAsync<int>(countQuery, param);
        var complaints = await connection.QueryAsync<ComplaintDto>(query, param: param);

        return new PagedList<ComplaintDto>(complaints, count, parameters.PageNumber, parameters.PageSize);
    }
}