namespace entities_library.publishing;

public class PublishingUser
{
    public int Id { get; set; } // Id de la publicación
    
    public int IdUser { get; set; } // Id del usuario que publicó
    
    public string? UserName { get; set; } // Nombre del usuario (puede ser opcional, útil para mostrar en la vista)
    
    public string? Description { get; set; } // Descripción de la publicación
    
    public byte[] Content { get; set; } // Contenido de la publicación (por ejemplo, una imagen en formato byte[])
    
    public DateTime PublishDate { get; set; } // Fecha de publicación
    
    public int CommentsCount { get; set; } // Contador de comentarios.


}