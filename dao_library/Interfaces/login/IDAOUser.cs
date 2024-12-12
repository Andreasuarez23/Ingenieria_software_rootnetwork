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

    
    
    //NO PROGRAMED
    Task Delete(User user);
    Task<User?> Get(string userName, string password);
    Task Update(User user);
}