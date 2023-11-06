namespace Entities.Models;

public class User
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? MotherName { get; set; }
    public string? ProvinceOfBirth { get; set; }
    public bool Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmergencyPhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? XAccount { get; set; }
    public string? FacebookAccount { get; set; }
    public string? InstagramAccount { get; set; }
    public string? LinkedInAccount { get; set; }
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public string? Distinct { get; set; }
    public string? StreetNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string? NationalIdNumber { get; set; }
    public string? ResidenceCardNumber { get; set; }
    public string? VoterCardNumber { get; set; }
    public int AddedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RecordDate { get; set; }
}