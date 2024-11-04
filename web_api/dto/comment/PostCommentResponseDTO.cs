using entities_library.login;
using web_api.dto.common;
using web_api.dto.login;


namespace web_api.dto.comment;

public class PostCommentResponseDTO : ResponsetDTO
{
    public long Id{get; set;}

    public required string Text {get; set;}

    public required User User  {get; set;}

    public DateTime Date {get; set;}
}