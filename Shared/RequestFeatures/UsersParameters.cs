namespace Shared.RequestFeatures;

public class UsersParameters : RequestParameters
{
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public int RegionId { get; set; }

    public string? SearchText { get; set; }
    public bool? Gender { get; set; }
    public DateTime? FromDateOfBirth { get; set; }
    public DateTime? ToDateOfBirth { get; set; }
    public DateTime? FromJoiningDate { get; set; }
    public DateTime? ToJoiningDate { get; set; }
}