using entities_library.publishing;

namespace dao_library.Interfaces.publishing;
public interface IDAOPublishing
{
    Task<(IEnumerable<Publishing> posts, int totalRecords)> GetAll(string? query, int page, int pageSize);

    Task<Publishing> GetById(long id);
    Task Save(Publishing publishing);
    
    Task AddPost(Publishing publishing);

    //NO PROGRAMAR
    Task Delete(Publishing publishing);
}
