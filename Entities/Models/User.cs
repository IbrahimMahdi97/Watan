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
    public string? WhatsAppNumber { get; set; }
    public string? EmergencyPhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? XAccount { get; set; }
    public string? FacebookAccount { get; set; }
    public string? InstagramAccount { get; set; }
    public string? LinkedInAccount { get; set; }
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public string? District { get; set; }
    public string? StreetNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string? NationalIdNumber { get; set; }
    public string? ResidenceCardNumber { get; set; }
    public string? VoterCardNumber { get; set; }
    public float Rating { get; set; }
    public string? DeviceId { get; set; }
    public string? MaritalStatus { get; set; }
    public string? JobPlace { get; set; }
    public DateTime RecruitmentYear { get; set; }
    public string? JobTitle { get; set; }
    public string? JobSector { get; set; }
    public string? JobType { get; set; }
    public string? GraduatedYear { get; set; }
    public string? GraduatedFromDepartment { get; set; }
    public string? GraduatedFromCollege { get; set; }
    public string? GraduatedFromUniversity { get; set; }
    public string? AcademicAchievement { get; set; }
    public int StudyingYearsCount { get; set; }
    public int JobDegree { get; set; }
    public int FamilyMembersCount { get; set; }
    public int ChildrenCount { get; set; }
    public DateTime JoiningDate { get; set; }
    public string? ClanName { get; set; }
    public string? SubClanName { get; set; }
    public bool IsFamiliesOfMartyrs { get; set; }
    public string? MartyrRelationship { get; set; }
    public string? FinancialCondition { get; set; }
    public string? Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public int AddedByUserId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime RecordDate { get; set; }
}