using entities_library.publishing;

namespace dao_library.Interfaces.publishing;
public interface IDAOPublishing
{
    Task<IEnumerable<Publishing>> GetAll();
    Task<Publishing> GetById(long id);
    Task Save(Publishing publishing);
    
    Task AddPost(Publishing publishing);

    //NO PROGRAMAR
    Task Delete(Publishing publishing);
}
