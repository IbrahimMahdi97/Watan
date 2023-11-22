namespace Shared.DataTransferObjects;

public class UserRegionDto
{
    public int UserId { get; set; }
    public int ProvinceId { get; set; }
    public string? Province { get; set; }
    public int TownId { get; set; }
    public string? Town { get; set; }
    public int RegionId { get; set; }
    public string? Region { get; set; }
}