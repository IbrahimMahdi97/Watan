namespace Entities.Exceptions;

public class RegionNotFoundException : NotFoundException
{
    public RegionNotFoundException(int recordId)
        : base($"The region with id: {recordId} doesn't exist in the database.")
    {
    }
}