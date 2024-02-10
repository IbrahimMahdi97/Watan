namespace Shared.DataTransferObjects;

public class UserHierarchyDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string RoleDescription { get; set; } = null!;
    public IEnumerable<UserHierarchyDto> SubordinateManagers { get; set; } = Array.Empty<UserHierarchyDto>();
    public UserRegionDto Region { get; set; }
}