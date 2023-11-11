namespace Shared.DataTransferObjects;

public class UserDto
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? MotherName { get; set; }
    public string? ProvinceOfBirth { get; set; }
    public bool Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public IEnumerable<UserRoleDto>? Roles { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}