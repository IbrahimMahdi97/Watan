using Shared.DataTransferObjects;

namespace Interfaces;

public interface IUserRepository
{
    Task<UserDto?> FindByCredentials(string phonenumber, string password);
    Task<UserDto?> FindByCredentialsEmail(string email, string password);
    Task<int> FindIdByPhone(string phonenumber);
    Task<int> FindIdByEmail(string email);
    Task<UserDto?> FindById(int id);
    Task<int> CreateUser(UserForCreationDto userForCreationDto);
    Task AddUserRoles(List<UserRoleForCreation> userRoles, int id);
    Task UpdateRefreshToken(int id, string refreshToken, DateTime? refreshTokenExpiryTime);
    Task<IEnumerable<UserRoleDto>> GetUserRoles(int userId);
}