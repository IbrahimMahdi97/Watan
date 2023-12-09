namespace Shared.RequestFeatures;

public class UsersParameters : RequestParameters
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
    public int TownId { get; set; }
    public int RegionId { get; set; }
    public int RoleId { get; set; }
}