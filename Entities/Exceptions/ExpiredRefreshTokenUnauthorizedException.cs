namespace Entities.Exceptions;

public class ExpiredRefreshTokenUnauthorizedException : UnauthorizedException
{
    public ExpiredRefreshTokenUnauthorizedException() 
        : base("Refresh token is expired, please log-in again.")
    {
    }
}