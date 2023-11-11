namespace Shared.DataTransferObjects;

public class UserForManipulationDto
{
    public string FullName { get; set; } = null!;
    public string? MotherName { get; set; }  = null!;
    public string? ProvinceOfBirth { get; set; }  = null!;
    public bool Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public string? District { get; set; }
    public string? StreetNumber { get; set; } = null!;
    public string? HouseNumber { get; set; } = null!;
    public string? NationalIdNumber { get; set; } = null!;
    public string? ResidenceCardNumber { get; set; } = null!;
    public string? VoterCardNumber { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}