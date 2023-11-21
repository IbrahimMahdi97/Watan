namespace Shared.DataTransferObjects;

public class UserDetailsDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string? MotherName { get; set; }  = null!;
    public string? ProvinceOfBirth { get; set; }  = null!;
    public bool Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? District { get; set; } = string.Empty;
    public string? StreetNumber { get; set; } = null!;
    public string? HouseNumber { get; set; } = null!;
    public string? NationalIdNumber { get; set; } = null!;
    public string? ResidenceCardNumber { get; set; } = null!;
    public string? VoterCardNumber { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public IEnumerable<UserRoleDto>? Roles { get; set; }
    public IEnumerable<UserRegionDto>? Regions { get; set; }
    public TokenDto? UserToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}