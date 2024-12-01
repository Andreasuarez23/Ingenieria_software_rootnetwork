using entities_library.publishing;

namespace dao_library.Interfaces.publishing;

public interface IDAOPublishingUser
{
    Task<(List<PublishingUser>, int)> GetAll(int pageNumber, int pageSize);
    
    Task<PublishingUser?> GetById(long id); // Devuelve una publicación específica por su ID
    
    Task<PublishingUser> Save(PublishingUser publishingUser);

}