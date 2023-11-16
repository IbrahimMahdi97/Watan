using Shared.DataTransferObjects;

namespace Interfaces;

public interface IUserRepository
{
    Task<UserDto?> FindByCredentialsEmailOrPhoneNumber(string emailOrPhoneNumber, string password);
    Task<int> FindIdByEmailOrPhoneNumber(string emailOrPhoneNumber);
    Task<UserDto?> FindById(int id);
    Task<int> CreateUser(UserForCreationDto userForCreationDto);
    Task AddUserRoles(List<UserRoleForCreation> userRoles, int id);
    Task UpdateRefreshToken(int id, string refreshToken, DateTime? refreshTokenExpiryTime);
    Task<IEnumerable<UserRoleDto>> GetUserRoles(int userId);
}