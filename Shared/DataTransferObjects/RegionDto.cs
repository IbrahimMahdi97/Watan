namespace Shared.DataTransferObjects;

public class RegionDto : RegionForManipulationDto
{
    public int Id { get; set; }
    public int ProvinceId { get; set; }
}