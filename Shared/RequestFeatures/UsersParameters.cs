namespace Shared.RequestFeatures;

public class UsersParameters : RequestParameters
{
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public int RegionId { get; set; }
}