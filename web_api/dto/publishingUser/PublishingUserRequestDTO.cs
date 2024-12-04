using web_api.dto.common;

namespace web_api.dto.publishingUser;

public class PublishingUserRequestDTO : RequestDTO
{
    public string? UserName { get; set; } = "";
    
    public string? Description { get; set; } = "";
    
     public string? ImageUrl { get; set; } // Campo para la URL de la imagen  
    
    public DateTime PublishDate { get; set; } // Fecha de publicación
    
    public int CommentsCount { get; set; } // Contador de comentarios.
}