using Microsoft.AspNetCore.Http;

namespace Shared.DataTransferObjects;

public class UserForCreationDto : UserForManipulationDto
{
    public string Password { get; set; } = null!;
    public List<UserRoleForCreation> Roles { get; set; } = null!;
    public IFormFile? UserImage { get; set; }
}