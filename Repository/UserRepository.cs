using Dapper;
using Interfaces;
using Repository.Query;
using Shared.DataTransferObjects;
using Shared.Helpers;
using Shared.RequestFeatures;

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
        param.Add("ProvinceId", userForCreationDto.UserRegion.ProvinceId);
        param.Add("TownId", userForCreationDto.UserRegion.TownId);
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

    public async Task<UserDto?> FindByCredentialsEmailOrPhoneNumber(string emailOrPhoneNumber, string password)
    {
        const string query = UserQuery.UserByCredentialsEmailOrPhoneNumberQuery;
        using var connection = _context.CreateConnection();
        var user = await connection.QuerySingleOrDefaultAsync<UserDto>(query,
            new { EmailOrPhoneNumber = emailOrPhoneNumber, Password = password });
        return user;
    }

    public async Task<int> FindIdByEmailOrPhoneNumber(string emailOrPhoneNumber)
    {
        const string query = UserQuery.UserIdByEmailOrPhoneNumberQuery;
        using var connection = _context.CreateConnection();
        var user = await connection.QuerySingleOrDefaultAsync<int>(query,
            new { EmailOrPhoneNumber = emailOrPhoneNumber });
        return user;
    }

    public async Task<UserDetailsDto?> FindById(int id)
    {
        const string query = UserQuery.UserByIdQuery;
        using var connection = _context.CreateConnection();
        var user = await connection.QuerySingleOrDefaultAsync<UserDetailsDto>(query, new { Id = id });
        return user;
    }

    public async Task AddUserRoles(IEnumerable<int> userRoles, int id)
    {
        const string queryUserRoles = RoleQuery.InsertUserRolesQuery;
        using var connection = _context.CreateConnection();
        connection.Open();

        foreach (var roleId in userRoles)
        {
            var paramUserRoles = new DynamicParameters();
            paramUserRoles.Add("RoleId", roleId);
            paramUserRoles.Add("UserId", id);
            await connection.ExecuteAsync(queryUserRoles, paramUserRoles);
        }
    }

    public async Task AddUserRegion(UserRegionForCreationDto userRegion, int userId)
    {
        const string queryUserRoles = UserRegionQuery.InsertUserRegionQuery;
        var param = new DynamicParameters(userRegion);
        using var connection = _context.CreateConnection();
        connection.Open();
        param.Add("UserId", userId);
        await connection.ExecuteAsync(queryUserRoles, param);
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
        var roles = await connection.QueryAsync<UserRoleDto>(query, new { Id = userId });
        return roles.ToList();
    }

    public async Task<IEnumerable<UserRegionDto>> GetUserRegions(int userId)
    {
        const string query = UserRegionQuery.UserRegionsByUserIdQuery;
        using var connection = _context.CreateConnection();
        var regions = await connection.QueryAsync<UserRegionDto>(query, new { Id = userId });
        return regions.ToList();
    }

    public async Task UpdateDeviceId(int userId, string deviceId)
    {
        const string query = UserQuery.UpdateDeviceIdByIdQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { Id = userId, DeviceId = deviceId });
    }

    public async Task<string?> GetUserDeviceId(int id)
    {
        const string query = UserQuery.UserDeviceIdQuery;
        using var connection = _context.CreateConnection();
        connection.Open();
        return await connection.QueryFirstOrDefaultAsync<string>(query, new { Id = id });
    }

    public async Task UpdateRating(UserRatingForUpdateDto userRatingForUpdateDto)
    {
        const string query = UserQuery.UpdateRatingByIdQuery;
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, userRatingForUpdateDto);
    }

    public async Task<PagedList<UserForListingDto>> GetByParameters(UsersParameters parameters)
    {
        const string query = UserQuery.SelectByParametersQuery;
        const string countQuery = UserQuery.SelectCountByParametersQuery;

        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var param = new DynamicParameters(parameters);
        param.Add("skip", skip);

        using var connection = _context.CreateConnection();

        var count = await connection.QueryFirstOrDefaultAsync<int>(countQuery, param);
        var users = await connection.QueryAsync<UserForListingDto>(query, param: param);

        return new PagedList<UserForListingDto>(users, count, parameters.PageNumber, parameters.PageSize);
    }

    public async Task Update(UserForCreationDto userForCreationDto, int userId)
    {
        const string query = UserQuery.UpdateByIdQuery;
        const string deleteRolesQuery = UserQuery.DeleteRolesByIdQuery;
        const string deleteRegionsQuery = UserQuery.DeleteRegionsByIdQuery;

        var parameters = new DynamicParameters(userForCreationDto);
        parameters.Add("UserId", userId);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, parameters);
        await connection.ExecuteAsync(deleteRolesQuery, new { UserId = userId });
        await connection.ExecuteAsync(deleteRegionsQuery, new { UserId = userId });

        await AddUserRoles(userForCreationDto.Roles, userId);
        await AddUserRegion(userForCreationDto.UserRegion, userId);
    }

    public async Task UpdateChildren(IEnumerable<UserChildForManipulation> children, int userId)
    {
        const string deleteChildrenQuery = UserQuery.DeleteChildrenByIdQuery;

        var parameters = new DynamicParameters();
        parameters.Add("UserId", userId);

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(deleteChildrenQuery, new { UserId = userId });

        await AddUserChildren(children, userId);
    }

    public async Task<int> GetCountByProvinceIdAndTownId(int provinceId, int townId)
    {
        const string query = UserQuery.SelectCountByProvinceIdAndTownIdQuery;
        using var connection = _context.CreateConnection();
        connection.Open();
        return await connection.QueryFirstOrDefaultAsync<int>(query, new { ProvinceId = provinceId, TownId = townId });
    }

    public async Task<UsersCountDto> GetCountFromDateToDate(DateTime fromDate, DateTime toDate)
    {
        const string query = UserQuery.SelectCountFromDateToDateQuery;
        using var connection = _context.CreateConnection();
        connection.Open();
        return await connection.QueryFirstOrDefaultAsync<UsersCountDto>(query,
            new { FromDate = fromDate, ToDate = toDate });
    }

    public async Task AddUserChildren(IEnumerable<UserChildForManipulation> children, int id)
    {
        const string queryUserRoles = UserQuery.InsertUserChildQuery;
        using var connection = _context.CreateConnection();
        connection.Open();

        foreach (var child in children)
        {
            var paramUserRoles = new DynamicParameters();
            paramUserRoles.Add("ChildName", child.ChildName);
            paramUserRoles.Add("Age", child.Age);
            paramUserRoles.Add("UserId", id);
            await connection.ExecuteAsync(queryUserRoles, paramUserRoles);
        }
    }

    public async Task<IEnumerable<UserChildForManipulation>?> GetUserChildren(int userId)
    {
        const string query = UserQuery.UserChildrenByUserIdQuery;
        using var connection = _context.CreateConnection();
        var children = await connection.QueryAsync<UserChildForManipulation>(query, new { Id = userId });
        return children.ToList();
    }
}