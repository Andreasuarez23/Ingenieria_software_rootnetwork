using web_api.dto.common;

namespace web_api.dto.publishing;

public class PublishingRequestDTO : RequestDTO
{
   public string? Text { get; set; } = ""; // Texto del post
    
    public string? ImageUrl { get; set; } // URL de la imagen
    
    public long UserId { get; set; } // ID del usuario (requerido para crear el objeto User)

}