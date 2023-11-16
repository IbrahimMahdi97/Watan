namespace Shared.DataTransferObjects;

public class UserForAuthenticationDto
{
    public string EmailOrPhoneNumber { get; init; } = null!;
    public string Password { get; init; } = null!;
}