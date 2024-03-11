namespace Shared.RequestFeatures;

public class UsersParameters : RequestParameters
{
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public int RegionId { get; set; }


    public int? StudyingYearsCount { get; set; }
    public int? JobDegree { get; set; }
    public int? FamilyMembersCount { get; set; }
    public int? ChildrenCount { get; set; }

    public string? FullName { get; set; }
    public string? MotherName { get; set; }
    public string? ProvinceOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? WhatsAppNumber { get; set; }
    public string? EmergencyPhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? District { get; set; }
    public string? StreetNumber { get; set; }
    public string? HouseNumber { get; set; }
    public string? NationalIdNumber { get; set; }
    public string? ResidenceCardNumber { get; set; }
    public string? VoterCardNumber { get; set; }
    public string? AcademicAchievement { get; set; }
    public string? GraduatedFromUniversity { get; set; }
    public string? GraduatedFromCollege { get; set; }
    public string? GraduatedFromDepartment { get; set; }
    public string? GraduatedYear { get; set; }
    public string? JobType { get; set; }
    public string? JobSector { get; set; }
    public string? JobTitle { get; set; }
    public string? JobPlace { get; set; }
    public string? MaritalStatus { get; set; }
    public string? ClanName { get; set; }
    public string? SubClanName { get; set; }
    public string? MartyrRelationship { get; set; }
    public string? FinancialCondition { get; set; }

    public bool? Gender { get; set; }
    public bool? IsFamiliesOfMartyrs { get; set; }
    public DateTime? FromDateOfBirth { get; set; }
    public DateTime? ToDateOfBirth { get; set; }
    public DateTime? FromJoiningDate { get; set; }
    public DateTime? ToJoiningDate { get; set; }

    public DateTime? FromRecruitmentYear { get; set; }
    public DateTime? ToRecruitmentYear { get; set; }

    public string? SearchText { get; set; }
    
    public string? InvitedByUsername { get; set; }
    public string? VotingCenterName { get; set; }
    public string? VotingCenterNumber { get; set; }
    public string? PartnerName { get; set; }
    public bool IsReceivingSocialSecurity { get; set; }
}