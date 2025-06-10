namespace Shared.DataTransferObjects;

public class PostDetailsDto : PostDto
{
    public IEnumerable<PostCommentDto>? Comments { get; set; }
    public IEnumerable<LikeDto>? Likes { get; set; }
}