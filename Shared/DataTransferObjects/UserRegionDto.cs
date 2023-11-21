namespace Shared.DataTransferObjects;

public class UserRegionDto
{
    public int UserId { get; set; }
    public ProvinceDto? Province { get; set; }
    public TownDto? Town { get; set; }
    public RegionDto? Region { get; set; }
}