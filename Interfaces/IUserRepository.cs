using System.Collections;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Interfaces;

public interface IUserRepository
{
    Task<UserDto?> FindByCredentialsEmailOrPhoneNumber(string emailOrPhoneNumber, string password);
    Task<int> FindIdByEmailOrPhoneNumber(string emailOrPhoneNumber);
    Task<UserDetailsDto?> FindById(int id);
    Task<int> CreateUser(UserForCreationDto userForCreationDto, int userId);
    Task AddUserRoles(IEnumerable<int> userRoles, int id);
    Task AddUserRegion(UserRegionForCreationDto userRegion, int userId);
    Task UpdateRefreshToken(int id, string refreshToken, DateTime? refreshTokenExpiryTime);
    Task<IEnumerable<UserRoleDto>> GetUserRoles(int userId);
    Task<IEnumerable<UserRegionDto>> GetUserRegions(int userId);
    Task UpdateDeviceId(int userId, string deviceId);
    Task<string?> GetUserDeviceId(int userId);
    Task UpdateRating(UserRatingForUpdateDto userRatingForUpdateDto);
    Task<PagedList<UserForListingDto>> GetByParameters(UsersParameters parameters);
    Task Update(UserForCreationDto userForCreationDto, int userId);
    Task<int> GetCountByProvinceIdAndTownId(int provinceId, int townId);
    Task<UsersCountDto> GetCountFromDateToDate(DateTime fromDate, DateTime toDate);
    Task<IEnumerable<UserChildForManipulation>?> GetUserChildren(int userId);
    Task UpdateChildren(IEnumerable<UserChildForManipulation> children, int userId);

}