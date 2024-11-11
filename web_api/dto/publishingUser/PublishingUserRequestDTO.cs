using web_api.dto.common;

namespace web_api.dto.publishingUser;

public class PublishingUserRequestDTO : RequestDTO
{
    public string? UserName { get; set; } = "";
    
    public string? Description { get; set; } = "";
    
    public required byte[] Content { get; set; } 
    
    public DateTime PublishDate { get; set; } // Fecha de publicación
    
    public int CommentsCount { get; set; } // Contador de comentarios.
}