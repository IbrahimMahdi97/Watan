using System.Data;
using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;

namespace Repository;

public class PostRepository : IPostRepository
{
    private readonly DapperContext _context;
    
    public PostRepository(DapperContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<PostDto>> GetAllPosts(int userId)
    {
        const string query = PostQuery.AllPostsQuery;
        var param = new DynamicParameters();
        const string prefixQuery = PostTypeQuery.PostTypeIdByPrefixQuery;
        using var connection = _context.CreateConnection();
        connection.Open();

        var trans = connection.BeginTransaction();
        var typeId = await connection.QuerySingleAsync<int>(prefixQuery, new {Prefix = "NWS"}, trans);
        param.Add("TypeId", typeId);
        param.Add("UserId", userId);
        
        var posts = await connection.QueryAsync<PostDto>(query, param, trans);
        return posts.ToList();
    }

    public async Task<PostDetailsDto> GetPostById(int id)
    {
        const string query = PostQuery.PostById;
        using var connection = _context.CreateConnection();
        var post = await connection.QuerySingleOrDefaultAsync<PostDetailsDto>(query, new { Id = id });
        return post;
    }

    public async Task<(int, IDbConnection, IDbTransaction)> CreatePost(PostForManipulationDto postDto, int userId, string postType)
    {
        const string prefixQuery = PostTypeQuery.PostTypeIdByPrefixQuery;
        var prefixParam = new DynamicParameters();
        prefixParam.Add("Prefix", postType);
        const string query = PostQuery.InsertPostQuery;
        var param = new DynamicParameters(postDto);
        param.Add("AddedByUserId", userId);
        param.Add("CreateDate", DateTime.Now);
        var connection = _context.CreateConnection();
        connection.Open();

        var trans = connection.BeginTransaction();
        var typeId = await connection.QuerySingleAsync<int>(prefixQuery, prefixParam, transaction: trans);
        param.Add("TypeId", typeId);
        var id = await connection.QuerySingleAsync<int>(query, param, transaction: trans);

        return (id, connection, trans);
    }

    public async Task UpdatePost(int id, PostForManipulationDto postDto)
    {
        const string query = PostQuery.UpdatePostQuery;
        var param = new DynamicParameters(postDto);
        param.Add("Id", id);
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, param);
    }

    public async Task DeletePost(int id)
    {
        const string query = PostQuery.DeletePostQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = id });
    }
}