using web_api.dto.common;

namespace web_api.dto.publishing;

public class PublishingResponseDTO : ResponsetDTO
{
    public int Id { get; set; } // Identificador único del post
    
    public string? Text { get; set; } // Descripción opcional del post

    public string? ImageUrl { get; set; } // URL de la imagen asociada al post
    
    public DateTime PublishDate { get; set; } // Fecha de publicación

    public string? UserName { get; set; } // Nombre del usuario que creó el post

    public bool IsShared { get; set; } // Indica si la publicación es compartida

    public OriginalPostDTO? OriginalPost { get; set; }

}
