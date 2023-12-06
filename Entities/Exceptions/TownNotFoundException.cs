namespace Entities.Exceptions;

public class TownNotFoundException : NotFoundException
{
    public TownNotFoundException(int recordId)
        : base($"The town with id: {recordId} doesn't exist in the database.")
    {
    }
}