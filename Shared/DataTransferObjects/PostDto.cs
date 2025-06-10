namespace Shared.DataTransferObjects;

public class PostDto : PostForManipulationDto
{
    public int Id { get; set; }
    public int LikesCount { get; set; }
    public bool IsLikedByLoggedInUser { get; set; }
    public int CommentsCount { get; set; }
    public string? ImageUrl { get; set; }
}