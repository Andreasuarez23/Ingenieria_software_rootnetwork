using entities_library.login;

namespace dao_library.Interfaces.login;

public interface IDAOUser 
{
    Task<(IEnumerable<User>, int)> GetAll(
        string? query,
        int page,
        int pageSize
    );

    Task<User> GetById(long id);
    Task Save(User user);
    
    //NO PROGRAMAR
    Task Delete(User user);
    Task<User> Get(string userName, string password);
}