using entities_library.login;

namespace dao_library.Interfaces.login;

public interface IDAOUserBan
{
    Task<IEnumerable<UserBan>> GetAll();
    Task<UserBan> GetById(long id);
    Task Save(UserBan userBan);
    
    //NO PROGRAMAR
    Task Delete(UserBan userBan);
}