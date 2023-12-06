namespace Entities.Exceptions;

public class RegionWithoutTownException : BadRequestException
{
    public RegionWithoutTownException(int regionId)
        : base($"Cannot insert region with Id: {regionId}, without providing town Id")
    {
        
    }
}