using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

public class PostCommentRepository : IPostCommentRepository
{
    private readonly DapperContext _context;

    public PostCommentRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> Create(PostCommentForManiupulationDto postComment, int userId)
    {
        const string query = PostCommentsQuery.InsertQuery;
        var param = new DynamicParameters(postComment);
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

    public async Task<PostCommentDto> GetById(int commentId)
    {
        const string query = PostCommentsQuery.GetByIdQuery;
        using var connection = _context.CreateConnection();
        var comment = await connection.QuerySingleOrDefaultAsync<PostCommentDto>(query, new { Id = commentId });
        return comment;
    }

    public async Task Update(PostCommentForManiupulationDto postComment, int commentId)
    {
        const string query = PostCommentsQuery.UpdateQuery;
        var param = new DynamicParameters(postComment);
        param.Add("Id", commentId);
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, param);
    }

    public async Task<IEnumerable<PostCommentDto>> GetPostComments(int postId)
    {
        const string query = PostCommentsQuery.GetByPostIdQuery;
        using var connection = _context.CreateConnection();
        var comments = 
            await connection.QueryAsync<PostCommentDto>(query, new { PostId = postId });
        return comments.ToList();
    }

    public async Task Delete(int commentId)
    {
        const string query = PostCommentsQuery.DeleteQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new {Id = commentId});
    }
}