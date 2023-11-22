using Shared.DataTransferObjects;

namespace Service.Interface;

public interface IUserService
{
    Task<UserDetailsDto> GetById(int id);
    Task<UserDto> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<int> CreateUser(UserForCreationDto userForCreationDto, int userId);
    Task<TokenDto> RefreshToken(TokenDto tokenDto);
}