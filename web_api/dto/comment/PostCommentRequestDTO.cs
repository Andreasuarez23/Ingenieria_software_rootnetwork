namespace web_api.DTOs
{
    public class PostCommentRequestDTO
    {
        public int UserId { get; set; }
        
        public required string Text { get; set; }

    }
}
