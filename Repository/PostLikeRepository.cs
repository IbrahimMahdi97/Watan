using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

public class PostLikeRepository : IPostLikeRepository
{
    private readonly DapperContext _context;

    public PostLikeRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<bool> CheckIfExist(int postId, int userId)
    {
        const string query = PostLikeQuery.CheckIfLikeExistQuery;
        using var connection = _context.CreateConnection();
        var post = await connection.QuerySingleOrDefaultAsync<int>(query, new { PostId = postId, UserId = userId });
        if (post > 0)
            return true;
        return false;
    }

    public async Task Create(int postId, int userId)
    {
        const string query = PostLikeQuery.InsertQuery;
        using var connection = _context.CreateConnection();
        await connection.QuerySingleOrDefaultAsync<int>(query, new { PostId = postId, UserId = userId });
    }

    public async Task Delete(int postId, int userId)
    {
        const string query = PostLikeQuery.DeleteQuery;
        using var connection = _context.CreateConnection();
        await connection.QuerySingleOrDefaultAsync<int>(query, new { PostId = postId, UserId = userId });
    }
    
    public async Task<IEnumerable<LikeDto>> GetPostLikes(int postId)
    {
        const string query = PostLikeQuery.GetByPostIdQuery;
        using var connection = _context.CreateConnection();
        var likes = await connection.QueryAsync<LikeDto>(query, new { PostId = postId });
        return likes.ToList();
    }
}