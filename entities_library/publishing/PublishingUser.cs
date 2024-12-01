using entities_library.comment;

namespace entities_library.publishing;

public class PublishingUser
{
    public int Id { get; set; }

    public string? UserName { get; set; } // Nombre del usuario (opcional para la vista)

    public string? Description { get; set; } // Descripción de la publicación

    public required byte[] Content { get; set; } // Contenido de la publicación

    public DateTime? PublishDate { get; set; } // Fecha de publicación

    // Relación con comentarios
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
