using web_api.dto.common;
namespace web_api.dto
{
    public class PostCommentRequestDTO
    {
        public int UserId { get; set; }
        
        public required string Text { get; set; }

    }
}
