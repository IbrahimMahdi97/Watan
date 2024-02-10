namespace Shared.DataTransferObjects;

public class UserForManipulationDto
{
    public string FullName { get; set; } = null!;
    public string? MotherName { get; set; }  = null!;
    public string? ProvinceOfBirth { get; set; }  = null!;
    public bool Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    
    public string? District { get; set; }
    public string? StreetNumber { get; set; } = null!;
    public string? HouseNumber { get; set; } = null!;
    public string? NationalIdNumber { get; set; } = null!;
    public string? ResidenceCardNumber { get; set; } = null!;
    public string? VoterCardNumber { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? EmergencyPhoneNumber { get; set; }
    public string? Email { get; set; }
    
    public float Rating { get; set; }
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
}