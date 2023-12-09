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

    public async Task AddLike(int postId, int userId)
    {
        const string query = PostLikeQuery.InsertOrDeleteQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { PostId = postId, UserId = userId });
    }
    
    public async Task<IEnumerable<LikeDto>> GetPostLikes(int postId)
    {
        const string query = PostLikeQuery.GetByPostIdQuery;
        using var connection = _context.CreateConnection();
        var likes = await connection.QueryAsync<LikeDto>(query, new { PostId = postId });
        return likes.ToList();
    }
}