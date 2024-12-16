using web_api.dto.common;
namespace web_api.dto
{
    public class CommentRequestDTO
    {
        public int UserId { get; set; }
        
        public required string Text { get; set; }

        public int PublishingId {get; set;}

    }
}
