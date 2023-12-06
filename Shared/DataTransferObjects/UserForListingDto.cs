namespace Shared.DataTransferObjects;

public class UserForListingDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string ImageUrl { get; set; } = string.Empty;
    public IEnumerable<UserRoleDto>? Roles { get; set; }
    public IEnumerable<UserRegionDto>? Regions { get; set; }
}