namespace Entities.Exceptions;

public class PostNotFoundException : NotFoundException
{
    public PostNotFoundException(int postId)
        : base($"The post with id: {postId} doesn't exist in the database.")
    {
    }
}