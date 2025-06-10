namespace Entities.Exceptions;

public class InvalidRegionTownException : BadRequestException
{
    public InvalidRegionTownException(int regionId, int townId)
        : base($"Region with Id {regionId} does not belong to town with Id {townId}.")
    {
        
    }
}