using web_api.dto.common;

namespace web_api.dto.publishingUser;

public class PublishingUserResponseDTO : ResponsetDTO
{

    public int Id { get; set; } // Identificador único del post
  

    //public string UserName { get; set; } = ""; // Nombre del usuario que creó el post

    public string? Text { get; set; } // Descripción opcional del post

    public string? ImageUrl {get; set;}
    //public DateTime PublishDate { get; set; } // Fecha de publicación

    //public int CommentsCount { get; set; } // Número de comentarios asociados al post
}
