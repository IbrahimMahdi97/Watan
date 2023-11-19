namespace Shared.DataTransferObjects;

public class UserDto : UserForManipulationDto
{
    public int Id { get; set; }
    public IEnumerable<UserRoleDto>? Roles { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}