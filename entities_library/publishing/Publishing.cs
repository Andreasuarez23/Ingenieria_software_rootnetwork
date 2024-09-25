namespace entities_library.publishing;

public class Publishing
{
    public int Id { get; set; } // Id de la publicación 
    
    public int IdUser { get; set; } // Id del usuario que publicó
    
    public required string Description { get; set; }// Descripción de la publicación

    public byte[] Content { get => Content1; set => Content1 = value; } // Contenido de la publicación 

    public DateTime PublishDate { get; set; } // Fecha de publicación 
    
    public PublishingStatus Status { get; set; } // Estado de la publicación 

    public Publishing(byte[] content1)
    {
        Content1 = content1;
    }

    public byte[] Content1 { get; set; }
}