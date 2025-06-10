using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects;

public class UserForCreationDto : UserForManipulationDto
{
    public string Password { get; set; } = null!;
    public IEnumerable<int> Roles { get; set; } = null!;
    public UserRegionForCreationDto UserRegion{ get; set; } = null!;
    public IFormFile? UserImage { get; set; }
}