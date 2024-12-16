using entities_library.login;
using web_api.dto.common;
using web_api.dto.login;


namespace web_api.dto.comment;

public class CommentResponseDTO : ResponsetDTO
{
    public long Id{get; set;}

    public required string Text {get; set;}

    public string? UserName  {get; set;}

    public int PublishingId {get; set;}

    public DateTime Date {get; set;}
}