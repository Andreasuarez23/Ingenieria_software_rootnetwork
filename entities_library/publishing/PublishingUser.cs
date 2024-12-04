using entities_library.comment;

namespace entities_library.publishing;

public class PublishingUser
{
    public int Id { get; set; }

    public required string UserName { get; set; } // Nombre del usuario 

    public string? Description { get; set; } // Descripción de la publicación

    public string ?ImageUrl { get; set; }
   
    public DateTime? PublishDate { get; set; } // Fecha de publicación

    // Relación con comentarios
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
}
