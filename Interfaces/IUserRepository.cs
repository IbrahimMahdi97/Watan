using Shared.DataTransferObjects;

namespace Interfaces;

public interface IUserRepository
{
    Task<UserDto?> FindByCredentialsEmailOrPhoneNumber(string emailOrPhoneNumber, string password);
    Task<int> FindIdByEmailOrPhoneNumber(string emailOrPhoneNumber);
    Task<UserDetailsDto?> FindById(int id);
    Task<int> CreateUser(UserForCreationDto userForCreationDto);
    Task AddUserRoles(List<UserRoleForCreation> userRoles, int id);
    Task AddUserRegion(UserRegionForCreationDto userRegion, int userId);
    Task UpdateRefreshToken(int id, string refreshToken, DateTime? refreshTokenExpiryTime);
    Task<IEnumerable<UserRoleDto>> GetUserRoles(int userId);
    Task<IEnumerable<UserRegionDto>> GetUserRegions(int userId);
    Task UpdateDeviceId(int userId, string deviceId);
    Task<string?> GetUserDeviceId(int userId);
}