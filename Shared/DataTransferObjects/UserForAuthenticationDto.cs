namespace Shared.DataTransferObjects;

public class UserForAuthenticationDto
{
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}