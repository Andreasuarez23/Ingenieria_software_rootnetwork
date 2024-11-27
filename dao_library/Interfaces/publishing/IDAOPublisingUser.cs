using entities_library.publishing;

namespace dao_library.Interfaces.publishing;

public interface IDAOPublishingUser
{
    Task<IEnumerable<PublishingUser>> GetAll(); // Devuelve todas las publicaciones del usuario
    
    Task<PublishingUser?> GetById(long id); // Devuelve una publicación específica por su ID
    
    Task<PublishingUser> Save(PublishingUser publishingUser);
   // Task<PublishingUser> Delete(PublishingUser publishingUser);

}