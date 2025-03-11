using entities_library.login;
namespace dao_library.Interfaces.login;

public interface IDAOUserBan
{
    Task<(IEnumerable<UserBan>, int)> GetAll(
        string? query,
        int page,
        int pageSize
    );
    
    Task<UserBan?> GetById(long id);
    Task Save(UserBan userBan);
    Task Unlock(long userBanId);
    Task Update(UserBan userBan);

    Task Delete(UserBan userBan);

    Task<UserBan?> GetActiveBanByUserId(long userId);
}