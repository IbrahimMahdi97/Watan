using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;
using Shared.Helpers;

namespace Repository;

public class UserRepository : IUserRepository
{
    private readonly DapperContext _context;

    public UserRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<int> CreateUser(UserForCreationDto userForCreationDto)
    {
        const string query = UserQuery.CreateUserQuery;
        var param = new DynamicParameters(userForCreationDto);

        using var connection = _context.CreateConnection();
        connection.Open();

        using var trans = connection.BeginTransaction();
        var id = await connection.QuerySingleAsync<int>(query, param, transaction: trans);

        const string passwordQuery = UserQuery.AddEncryptedPasswordByIdQuery;
        await connection.ExecuteAsync(passwordQuery, new
        {
            Password = (userForCreationDto.Password + id).ToSha512(),
            Id = id
        }, transaction: trans);
        
        trans.Commit();
        return id;
    }

    public async Task<UserDto?> FindByCredentials(string phonenumber, string password)
    {
        const string query = UserQuery.UserByCredentialsQuery;
        using var connection = _context.CreateConnection();
        var user = await connection.QuerySingleOrDefaultAsync<UserDto>(query,
            new { PhoneNumber = phonenumber, Password = password });
        return user;
    }

    public async Task<int> FindIdByPhone(string phonenumber)
    {
        const string query = UserQuery.UserIdByPhoneQuery;
        using var connection = _context.CreateConnection();
        var user = await connection.QuerySingleOrDefaultAsync<int>(query, new { PhoneNumber = phonenumber });
        return user;
    }
    
    public async Task<UserDto?> FindById(int id)
    {
        const string query = UserQuery.UserByIdQuery;
        using var connection = _context.CreateConnection();
        var user = await connection.QuerySingleOrDefaultAsync<UserDto>(query, new { Id = id });
        return user;
    }

    public async Task AddUserRoles(List<UserRoleForCreation> userRoles, int id)
    {
        const string queryUserRoles = RoleQuery.InsertUserRolesQuery;
        using var connection = _context.CreateConnection();
        connection.Open();

        foreach (var paramUserRoles in userRoles.Select(role => new DynamicParameters(role)))
        {
            paramUserRoles.Add("UserId", id);
            await connection.ExecuteAsync(queryUserRoles, paramUserRoles);
        }
    }
    
    public async Task UpdateRefreshToken(int id, string refreshToken, DateTime? refreshTokenExpiryTime)
    {
        const string query = UserQuery.UpdateRefreshTokenByIdQuery;

        using var connection = _context.CreateConnection();
        connection.Open();

        using var trans = connection.BeginTransaction();
        await connection.ExecuteAsync(query,
            new { Id = id, RefreshToken = refreshToken, RefreshTokenExpiryTime = refreshTokenExpiryTime },
            transaction: trans);

        trans.Commit();
    }
    public async Task<IEnumerable<UserRoleDto>> GetUserRoles(int userId)
    {
        const string query = UserQuery.UserRolesByUserIdQuery;
        using var connection = _context.CreateConnection();
        var cities = await connection.QueryAsync<UserRoleDto>(query, new { Id = userId });
        return cities.ToList();
    }
}